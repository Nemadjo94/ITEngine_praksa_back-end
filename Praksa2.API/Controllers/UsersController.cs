using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Praksa2.Repo.Models;
using Praksa2.Service;
using Praksa2.Service.Dtos;

namespace Praksa2.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserServices _userServices;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserServices userServices, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userServices = userServices;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UsersDto userDto)
        {
            var user = _userServices.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // Handle our JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return basic user info ( without password ) and token to store to client side
            return Ok(new
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Phonenumber = user.PhoneNumber,
                Photo = user.Photo,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UsersDto usersDto)
        {
            // Map dto to entity
            var user = _mapper.Map<Users>(usersDto);

            try
            {
                // Save
                _userServices.Create(user, usersDto.Password);
                return Ok();
            }
            catch(AppException ex)
            {
                // Return error message if there's an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            // Allow only admins to get all user records
            if (!User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            var users = _userServices.GetAll();
            var userDtos = _mapper.Map<IList<UsersDto>>(users);
            return Ok(userDtos);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userServices.GetById(id);

            // Allow only admins to get other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UsersDto>(user);
            return Ok(userDto);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UsersDto userDto)
        {
            // Map dto to entity and set id
            var user = _mapper.Map<Users>(userDto);
            user.ID = id;

            // Allow only admins to update other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            try
            {
                // Save
                _userServices.Update(user, userDto.Password);
                return Ok();
            }
            catch(AppException ex)
            {
                // Return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userServices.Delete(id);

            // Allow only admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if(id != currentUserId && !User.IsInRole(Roles.Admin))
            {
                return Forbid();
            }

            return Ok();
        }


    }
}

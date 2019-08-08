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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Praksa2.Repo.Models;
using Praksa2.Service;
using Praksa2.Service.Dtos;
using Microsoft.Extensions.Configuration;

namespace Praksa2.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private IUserServices _userServices;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(IUserServices userServices, IMapper mapper, IOptions<AppSettings> appSettings, UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            _userServices = userServices;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<object> Authenticate([FromBody]UsersDto userDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userDto.Email, userDto.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == userDto.Email);
                return await GenerateJwtToken(userDto.Email, appUser);
            }

            throw new AppException("INVALID_AUTHENTICATION");
        }

        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public async Task<object> CreateUser([FromBody]UsersDto usersDto)
        //{
        //    var user = new IdentityUser
        //    {
        //        f
        //    };
        //}

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<object> Register([FromBody]UsersDto usersDto)
        {
            // Map dto to entity
            var user = new IdentityUser {
                UserName = usersDto.Email,
                Email = usersDto.Email,
            };

            var result = await _userManager.CreateAsync(user, usersDto.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(usersDto.Email, user);            
            }

            throw new AppException("Unknown Error");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            // Allow only admins to get all user records
            if (!User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var users = _userServices.GetAll();
            var userDtos = _mapper.Map<IList<UsersDto>>(users);
            return Ok(userDtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());

            // Allow only admins to get other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            if (result == null)
            {
                return NotFound();
            }

            
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UsersDto userDto)
        {
            // Map dto to entity and set id
            var user = _mapper.Map<Users>(userDto);
            user.Id = id.ToString();

            // Allow only admins to update other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole("Admin"))
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task <IActionResult> Delete(int id)
        {
            _userServices.Delete(id);

            // Allow only admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if(id != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            return Ok();
        }

        private async Task<object> GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


    }
}

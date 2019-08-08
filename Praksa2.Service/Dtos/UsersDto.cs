using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Praksa2.Service.Dtos
{
    /// <summary>
    /// The user DTO is a data transfer object used send selected user data to and from the users api end points.
    /// </summary>
    public class UsersDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public byte[] Photo { get; set; }
 
    }
}

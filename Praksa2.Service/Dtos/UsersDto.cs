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
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public byte[] Photo { get; set; }
 
    }
}

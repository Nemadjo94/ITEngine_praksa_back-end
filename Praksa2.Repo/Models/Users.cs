using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Praksa2.Repo.Models
{
    public class Users
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Role { get; set; }
        //Required for soft deleting
        public bool isDeleted { get; set; }

        public virtual ICollection<Users> UsersCollection { get; set; }
    }

}

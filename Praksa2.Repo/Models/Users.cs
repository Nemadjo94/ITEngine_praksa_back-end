
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Praksa2.Repo.Models
{
    public class Users : IdentityUser
    {

        public Users() : base() { }
 
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        public string Photo { get; set; }
        //Required for soft deleting
        public bool IsDeleted { get; set; }

        //public virtual ICollection<Users> UsersCollection { get; set; }
    }

}

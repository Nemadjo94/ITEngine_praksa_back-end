using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praksa2.Repo.Models
{
    /// <summary>
    /// Role entity class. 
    /// </summary>
    public class Roles : IdentityRole
    {
        // Description for the given role
        public string Description { get; set; }
        // Creation date for the given role
        public DateTime CreationDate { get; set; }

        public Roles() : base() {}

        public Roles(string roleName) : base(roleName)
        {

        }

        public Roles(string roleName, string description, DateTime creationDate) : base(roleName)
        {

        }
    }
}

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
        //public string Description { get; set; }

        public Roles() : base() {}

        public Roles(string roleName) : base(roleName)
        {

        }
    }
}

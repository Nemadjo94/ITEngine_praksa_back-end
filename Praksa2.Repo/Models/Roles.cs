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
        public Roles() : base() {}

        public Roles(string roleName) : base(roleName)
        {

        }
    }
}

using Praksa2.Repo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Praksa2.Service
{
    public interface IUserServices
    {
        Users Authenticate(string username, string password);
        IEnumerable<Users> GetAll();
        Users GetById(int id);
        Users Create(Users user, string password);
        void Update(Users user, string password = null);
        void Delete(int id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Praksa2.Repo;
using Praksa2.Repo.Models;


namespace Praksa2.Service
{
    public class UserServices : IUserServices
    {
        private Context _context;
        public UserServices(Context context)
        {
            _context = context;
        }
        public Users Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // Check if username exists
            if (user == null)
                return null;

            // Check if password is correct
            if(password != user.Password)// change after inserting identity
            {
                return null;
            }

            // Succesful authentication
            return user;

        }

        public Users Create(Users user, string password)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if(_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            user.Password = password;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public Users GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Users user, string password = null)
        {
            throw new NotImplementedException();
        }
    }
}

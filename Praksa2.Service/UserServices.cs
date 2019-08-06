﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Praksa2.Repo;
using Praksa2.Repo.Models;
using Praksa2.Service.Helpers;

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
            // If input is empty just return null
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // Check if username exists
            if (user == null)
                return null;

            // Check if password is correct
            if(!PasswordHandler.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            // Succesful authentication
            return user;

        }

        public Users Create(Users user, string password)
        {
            // Validate password
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if(_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            PasswordHandler.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            // Find user by id
            var user = _context.Users.Find(id);
            if(user != null)
            {
                _context.Users.Remove(user); //Change to soft deletion later
                _context.SaveChanges();
            }
        }

        public IEnumerable<Users> GetAll()
        {
            return _context.Users;
        }

        public Users GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(Users userParam, string password = null)
        {
            // Find user by id
            var user = _context.Users.Find(userParam.ID);

            // If there is no user throw app exception
            if (user == null)
                throw new AppException("User not found");

            if(userParam.Username != user.Username)
            {
                // Username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.Username == userParam.Username))
                    throw new AppException("Username " + userParam.Username + " is already taken");
            }

            // Update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Email = userParam.Email;
            user.PhoneNumber = userParam.PhoneNumber;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                PasswordHandler.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();

        }

    }
}

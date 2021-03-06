﻿using BL.Managers.Interfaces;
using Data.Database;
using Data.Repositories.Interfaces;
using Model.Models;
using Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class UserManagerNoDI : IUserManager
    {
        private IUserRepository _userRepository;

        public UserManagerNoDI(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDisplayDto CreateUser(UserRegisterDto user)
        {

            User createdUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = Util.HashHelper.GetMD5HashData(user.Password),
                UserName = user.UserName,
                CreatedOn = DateTime.Now
            };
 
            createdUser = _userRepository.Add(createdUser);

            UserDisplayDto displayUser = new UserDisplayDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return displayUser;
        }

        public User FindUser(string userName, string password)
        {
            var passwordHash = Util.HashHelper.GetMD5HashData(password);

            return _userRepository.FindUser(userName, passwordHash);
        }

    }

}

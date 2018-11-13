using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Dtos;

namespace BL.Managers.Interfaces
{
    public interface IUserManager
    {
        UserDisplayDto CreateUser(UserRegisterDto user);
        User FindUser(string userName, string password);
    }
}

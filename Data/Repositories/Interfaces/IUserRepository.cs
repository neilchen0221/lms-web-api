using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User FindUser(string userName, string passwordHash);
    }

}
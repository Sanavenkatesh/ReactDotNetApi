using ReactDotNetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetApi.Data
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}

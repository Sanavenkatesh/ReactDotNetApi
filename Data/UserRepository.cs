using ReactDotNetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public User Create(User user)
        {
            _context.Users.Add(user);
            user.Id = _context.SaveChanges();
            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}

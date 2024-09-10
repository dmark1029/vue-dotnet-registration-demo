using System.Collections.Generic;
using System.Linq;
using AuthAPI.Models;

namespace AuthAPI.Repositories
{
    public class InMemoryUserRepository
    {
        private static readonly List<User> Users = new List<User>();

        public User GetUserByUsername(string username)
        {
            return Users.SingleOrDefault(user => user.Username == username) ?? new User { Username = "Unknown", Email = "", PasswordHash = "" };
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public bool UserExists(string username)
        {
            return Users.Any(u => u.Username == username);
        }
    }
}
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public void Create(User user);
        public User CheckExistingUser(string email);

        //public void UpdateUser(int id, User user);
        //public void DeleteUser(int id);
        //public List<User> SearchUser(string value);

    }
}

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserById(long id);
        public void Create(User user);
        //bool Update(User user);
        //bool Delete(long id);
        public User GetByEmail(string email);
    }
}

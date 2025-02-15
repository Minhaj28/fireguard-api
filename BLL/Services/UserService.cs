using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void Create(User user)
        {
            _userRepository.Create(user);
        }

        public User CheckExistingUser(string email) 
        { 
           return _userRepository.GetByEmail(email);
        }

        //public void UpdateUser(int id, User user)
        //{
        //    _userAction.UpdateUser(id, user);
        //}

        //public void DeleteUser(int id)
        //{
        //    _userAction.DeleteUser(id);
        //}

    }
}

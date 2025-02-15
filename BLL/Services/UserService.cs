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

        public User Signin(User user) 
        {
            User user1 = _userRepository.GetByEmail(user.Email);
            try
            {
                if(user.Email == user1.Email && user.PasswordHash == user1.PasswordHash)
                {
                    return user;
                }
                else
                {
                    throw new Exception($"User not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
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

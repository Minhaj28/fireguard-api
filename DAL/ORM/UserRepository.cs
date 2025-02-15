using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DAL.Interfaces;

namespace DAL.ORM
{
    public class UserRepository : IUserRepository
    {
        protected List<User> users = new List<User>();

        public List<User> GetAllUsers()
        {
            string _cmdSelect = "select * from user";

            OdbcCommand cmd = new OdbcCommand(_cmdSelect);
            List<User> userList = GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);

            return userList;
        }

        internal List<User> GetAsList(DataTable dt)
        {
            List<User> ItemList = new List<User>();
            foreach (DataRow dr in dt.Rows)
            {
                User ItemObj = new User();
                if (!string.IsNullOrEmpty(dr["Id"].ToString()))
                    ItemObj.Id = Convert.ToInt64(dr["Id"]);

                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    ItemObj.Name = Convert.ToString(dr["Name"]);

                if (!string.IsNullOrEmpty(dr["Email"].ToString()))
                    ItemObj.Email = Convert.ToString(dr["Email"]);

                if (!string.IsNullOrEmpty(dr["PasswordHash"].ToString()))
                    ItemObj.PasswordHash = Convert.ToString(dr["PasswordHash"]);

                ItemList.Add(ItemObj);
            }
            return ItemList;
        }

        public User GetUserById(long id)
        {
            try
            {
                string _cmdSelect = "select * from user WHERE Id = ?";

                OdbcCommand cmd = new OdbcCommand(_cmdSelect);

                cmd.Parameters.AddWithValue("@Id", id);

                List<User> userList = GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);

                if(userList.Count == 0)
                {
                    throw new Exception($"User with Id {id} not found.");
                }

                return userList[0];
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public void Create(User user)
        {
            string _cmdInsert = "INSERT INTO User (Name, Email, PasswordHash) VALUES (?, ?, ?)";

            OdbcCommand cmd1 = new OdbcCommand(_cmdInsert);

            //cmd.CommandText(_cmdInsert);
            cmd1.Parameters.AddWithValue("@Name", user.Name);
            cmd1.Parameters.AddWithValue("@Email", user.Email);
            cmd1.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

            DBConnection.ExecuteNonQueryAndScalar(cmd1);
        }
        public User GetByEmail(string email)
        {
            try
            {
                string query = "SELECT Id,Name,Email,PasswordHash FROM user WHERE Email = ?";

                OdbcCommand cmd = new OdbcCommand(query);

                cmd.Parameters.AddWithValue("@Email", email);

                List<User> userList = GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);

                if (userList.Count == 0)
                {
                    throw new Exception($"User with email {email} not found.");
                }

                return userList[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void UpdateUser(int id, User user)
        //{
        //    try
        //    {
        //        string _cmdUpdate = "UPDATE user SET UserName = ?, UserPassword = ?  WHERE UserId = ?";

        //        // Create an OdbcCommand object
        //        OdbcCommand cmd1 = new OdbcCommand(_cmdUpdate);

        //        // Add parameters for the fields to be updated
        //        cmd1.Parameters.AddWithValue("@UserName", user.UserName);
        //        cmd1.Parameters.AddWithValue("@UserPassword", user.UserPassword);

        //        // Add the parameter for the ID
        //        cmd1.Parameters.AddWithValue("@UserId", id);

        //        // Execute the update command
        //        DBConnection.ExecuteNonQueryAndScalar(cmd1);



        //    }
        //    catch (Exception Ex)
        //    {

        //        throw Ex;
        //    }
        //}

        //public void DeleteUser(int id)
        //{
        //    try
        //    {
        //        // Define the SQL delete command with a placeholder for the ID
        //        string _cmdDelete = "DELETE FROM User WHERE UserId = ?";

        //        // Create an OdbcCommand object
        //        OdbcCommand cmd1 = new OdbcCommand(_cmdDelete);

        //        // Add the parameter for the ID
        //        cmd1.Parameters.AddWithValue("@UserId", id);

        //        // Execute the delete command
        //        DBConnection.ExecuteNonQueryAndScalar(cmd1);

        //    }
        //    catch (Exception Ex)
        //    {

        //        throw Ex;
        //    }
        //}
    }
}

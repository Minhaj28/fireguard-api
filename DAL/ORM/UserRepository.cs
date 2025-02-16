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
using System.Numerics;

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
                    ItemObj.Id = Convert.ToInt32(dr["Id"]);

                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                    ItemObj.Name = Convert.ToString(dr["Name"]);

                if (!string.IsNullOrEmpty(dr["Email"].ToString()))
                    ItemObj.Email = Convert.ToString(dr["Email"]);

                if (!string.IsNullOrEmpty(dr["PasswordHash"].ToString()))
                    ItemObj.PasswordHash = Convert.ToString(dr["PasswordHash"]);

                if (!string.IsNullOrEmpty(dr["Phone"].ToString()))
                    ItemObj.Phone = Convert.ToString(dr["Phone"]);
                

                ItemList.Add(ItemObj);
            }
            return ItemList;
        }

        public User GetUserById(int id)
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

    public class BuildingRepository : IBuildingRepository
    {
        public List<Building> GetAll()
        {
            string query = "SELECT * FROM Building";
            OdbcCommand cmd = new OdbcCommand(query);
            return GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);
        }

        private List<Building> GetAsList(DataTable dt)
        {
            List<Building> buildings = new List<Building>();
            foreach (DataRow dr in dt.Rows)
            {
                buildings.Add(new Building
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Address = dr["Address"].ToString()
                });
            }
            return buildings;
        }
    }

    public class FloorRepository : IFloorRepository
    {
        public List<Floor> GetAll()
        {
            string query = "SELECT * FROM Floor";
            OdbcCommand cmd = new OdbcCommand(query);
            return GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);
        }

        private List<Floor> GetAsList(DataTable dt)
        {
            List<Floor> floors = new List<Floor>();
            foreach (DataRow dr in dt.Rows)
            {
                floors.Add(new Floor
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    BuildingId = Convert.ToInt32(dr["BuildingId"]),
                    FloorNumber = Convert.ToInt32(dr["FloorNumber"])
                });
            }
            return floors;
        }
    }

    public class CameraRepository : ICameraRepository
    {
        public List<Camera> GetAll()
        {
            string query = "SELECT * FROM Camera";
            OdbcCommand cmd = new OdbcCommand(query);
            return GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);
        }

        private List<Camera> GetAsList(DataTable dt)
        {
            List<Camera> cameras = new List<Camera>();
            foreach (DataRow dr in dt.Rows)
            {
                cameras.Add(new Camera
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FloorId = Convert.ToInt32(dr["FloorId"]),
                    Location = dr["Location"].ToString(),
                    LastImageUrl = dr["LastImageUrl"].ToString(),
                    StatusId = Convert.ToInt32(dr["StatusId"])
                });
            }
            return cameras;
        }
    }

    public class SensorRepository : ISensorRepository
    {
        public List<Sensor> GetAll()
        {
            string query = "SELECT * FROM Sensor";
            OdbcCommand cmd = new OdbcCommand(query);
            return GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);
        }

        private List<Sensor> GetAsList(DataTable dt)
        {
            List<Sensor> sensors = new List<Sensor>();
            foreach (DataRow dr in dt.Rows)
            {
                sensors.Add(new Sensor
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FloorId = Convert.ToInt32(dr["FloorId"]),
                    Location = dr["Location"].ToString(),
                    SensorTypeId = Convert.ToInt32(dr["SensorTypeId"]),
                    StatusId = Convert.ToInt32(dr["StatusId"]),
                    LastValue = dr["LastValue"] != DBNull.Value ? Convert.ToSingle(dr["LastValue"]) : (float?)null
                });
            }
            return sensors;
        }
    }

    public class IncidentRepository : IIncidentRepository
    {
        public List<Incident> GetAll()
        {
            string query = "SELECT * FROM Incident";
            OdbcCommand cmd = new OdbcCommand(query);
            return GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);
        }

        private List<Incident> GetAsList(DataTable dt)
        {
            List<Incident> incidents = new List<Incident>();
            foreach (DataRow dr in dt.Rows)
            {
                incidents.Add(new Incident
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    FloorId = Convert.ToInt32(dr["FloorId"]),
                    DetectedById = Convert.ToInt32(dr["DetectedById"]),
                    ImageUrl = dr["ImageUrl"].ToString(),
                    SensorId = dr["SensorId"] != DBNull.Value ? Convert.ToInt32(dr["SensorId"]) : (int?)null,
                    StatusId = Convert.ToInt32(dr["StatusId"])
                });
            }
            return incidents;
        }
    }

    public class EmergencyActionRepository : IEmergencyActionRepository
    {
        public List<EmergencyAction> GetAll()
        {
            string query = "SELECT * FROM EmergencyAction";
            OdbcCommand cmd = new OdbcCommand(query);
            return GetAsList(DBConnection.ExecuteQuery(cmd).Tables[0]);
        }

        private List<EmergencyAction> GetAsList(DataTable dt)
        {
            List<EmergencyAction> actions = new List<EmergencyAction>();
            foreach (DataRow dr in dt.Rows)
            {
                actions.Add(new EmergencyAction
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    IncidentId = Convert.ToInt32(dr["IncidentId"]),
                    ActionTypeId = Convert.ToInt32(dr["ActionTypeId"])
                });
            }
            return actions;
        }
    }
}

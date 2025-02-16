using DAL.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public void Create(User user);
        //bool Update(User user);
        //bool Delete(long id);
        public User GetByEmail(string email);
    }

    public interface IBuildingRepository
    {
        List<Building> GetAll();
    }

    public interface IFloorRepository
    {
        List<Floor> GetAll();
    }

    public interface ICameraRepository
    {
        List<Camera> GetAll();
    }

    public interface ISensorRepository
    {
        List<Sensor> GetAll();
    }

    public interface IIncidentRepository
    {
        List<Incident> GetAll();
    }

    public interface IEmergencyActionRepository
    {
        List<EmergencyAction> GetAll();
    }
}

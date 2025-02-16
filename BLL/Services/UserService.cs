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

    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingService(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public List<Building> GetAllBuildings()
        {
            return _buildingRepository.GetAll();
        }
    }

    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;

        public FloorService(IFloorRepository floorRepository)
        {
            _floorRepository = floorRepository;
        }

        public List<Floor> GetAllFloors()
        {
            return _floorRepository.GetAll();
        }
    }

    public class CameraService : ICameraService
    {
        private readonly ICameraRepository _cameraRepository;

        public CameraService(ICameraRepository cameraRepository)
        {
            _cameraRepository = cameraRepository;
        }

        public List<Camera> GetAllCameras()
        {
            return _cameraRepository.GetAll();
        }
    }

    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;

        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public List<Sensor> GetAllSensors()
        {
            return _sensorRepository.GetAll();
        }
    }

    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentService(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        public List<Incident> GetAllIncidents()
        {
            return _incidentRepository.GetAll();
        }
    }

    public class EmergencyActionService : IEmergencyActionService
    {
        private readonly IEmergencyActionRepository _emergencyActionRepository;

        public EmergencyActionService(IEmergencyActionRepository emergencyActionRepository)
        {
            _emergencyActionRepository = emergencyActionRepository;
        }

        public List<EmergencyAction> GetAllEmergencyActions()
        {
            return _emergencyActionRepository.GetAll();
        }
    }

}

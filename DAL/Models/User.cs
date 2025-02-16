using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User
    {
        public User() { }

        public int Id { get; set; }
        public  string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; } = string.Empty;
    }

    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class Floor
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int FloorNumber { get; set; }
    }

    public class Camera
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string LastImageUrl { get; set; } = string.Empty;
        public int StatusId { get; set; }
    }

    public class Sensor
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public string Location { get; set; } = string.Empty;
        public int SensorTypeId { get; set; }
        public int StatusId { get; set; }
        public float? LastValue { get; set; }
    }

    public class SensorType
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
    }

    public class StatusType
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }

    public class Incident
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public int DetectedById { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int? SensorId { get; set; }
        public int StatusId { get; set; }
    }

    public class DetectionType
    {
        public int Id { get; set; }
        public string DetectionMethod { get; set; } = string.Empty;
    }

    public class IncidentStatus
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }

    public class EmergencyAction
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public int ActionTypeId { get; set; }
    }

    public class EmergencyActionType
    {
        public int Id { get; set; }
        public string ActionName { get; set; } = string.Empty;
    }

}

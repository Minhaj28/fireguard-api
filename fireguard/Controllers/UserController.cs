using BLL.Interfaces;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace fireguard.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<User> users = _userService.GetAllUsers();
                if (users == null || users.Count == 0)
                {
                    return NotFound("No users found."); // 404 Not Found
                }


                return Ok(users); // 200 OK with the list of users
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                // Fetch the user by ID
                User user = _userService.GetUserById(id);

                // Check if the user was found
                if (user == null)
                {
                    return NotFound($"User with ID {id} not found."); // 404 Not Found
                }

                // Return the user details
                return Ok(user); // 200 OK with the user details
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null."); // 400 Bad Request
            }

            try
            {
                // Call the service to create the user
                _userService.Create(user);

                // Return a success response
                return Ok("User created successfully."); // 200 OK
            }
            catch (Exception ex)
            {
                // Log the exception and return a server error
                return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
            }
        }

        /// <summary>
        /// Checks if a user with a given email already exists.
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        [HttpGet("existing-user/email/{email}")]
        public IActionResult CheckExistingUser(string email)
        {
            try
            {
                User user = _userService.CheckExistingUser(email);
                if (user == null)
                {
                    return NotFound($"User with email {email} not found."); // 404 Not Found
                }

                // Return the user details
                return Ok(user); // 200 OK with the user details
             }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }

        /// <summary>
        /// Signin user
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns>user if the user exists.</returns>
        [HttpGet("signin")]
        public IActionResult UserSignIn([FromBody] User user)
        {
            try
            {

                // Return the user details
                return Ok(_userService.Signin(user)); // 200 OK with the user details
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval
                return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
            }
        }


        //// PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] User updatedUser)
        //{
        //    if (updatedUser == null)
        //    {
        //        return BadRequest("User data is null."); // 400 Bad Request
        //    }

        //    try
        //    {
        //        // Fetch the existing user by ID
        //        var existingUser = _userService.GetUserById(id);
        //        if (existingUser == null)
        //        {
        //            return NotFound($"User with ID {id} not found."); // 404 Not Found
        //        }

        //        // Update the user information
        //        _userService.UpdateUser(id, updatedUser);

        //        // Return a success message with 200 OK
        //        return Ok("User updated successfully."); // 200 OK
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and return a server error
        //        return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
        //    }
        //}


        //// DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        // Check if the user exists
        //        var user = _userService.GetUserById(id);
        //        if (user == null)
        //        {
        //            return NotFound($"User with ID {id} not found."); // 404 Not Found
        //        }

        //        // Delete related records if needed (e.g., assigned roles or permissions)
        //        _userService.DeleteUser(id);

        //        // Return a success message with 200 OK
        //        return Ok("User deleted successfully."); // 200 OK with success message
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}"); // 500 Internal Server Error
        //    }
        //}

        //[HttpGet("search")]
        //public IActionResult Search([FromQuery] string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        return BadRequest("Search value cannot be null or empty."); // 400 Bad Request
        //    }

        //    try
        //    {
        //        List<User> users = _userService.SearchUser(value);
        //        if (users == null || users.Count == 0)
        //        {
        //            return NotFound("No users found matching the search criteria."); // 404 Not Found
        //        }
        //        return Ok(users); // 200 OK with the list of users
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Internal server error: " + ex.Message); // 500 Internal Server Error
        //    }
        //}
    }

    [Route("api/building")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Building> buildings = _buildingService.GetAllBuildings();
                if (buildings == null || buildings.Count == 0)
                {
                    return NotFound("No buildings found.");
                }
                return Ok(buildings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

    [Route("api/floor")]
    [ApiController]
    public class FloorController : ControllerBase
    {
        private readonly IFloorService _floorService;

        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Floor> floors = _floorService.GetAllFloors();
                if (floors == null || floors.Count == 0)
                {
                    return NotFound("No floors found.");
                }
                return Ok(floors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

    [Route("api/camera")]
    [ApiController]
    public class CameraController : ControllerBase
    {
        private readonly ICameraService _cameraService;

        public CameraController(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Camera> cameras = _cameraService.GetAllCameras();
                if (cameras == null || cameras.Count == 0)
                {
                    return NotFound("No cameras found.");
                }
                return Ok(cameras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

    [Route("api/sensor")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Sensor> sensors = _sensorService.GetAllSensors();
                if (sensors == null || sensors.Count == 0)
                {
                    return NotFound("No sensors found.");
                }
                return Ok(sensors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

    [Route("api/incident")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Incident> incidents = _incidentService.GetAllIncidents();
                if (incidents == null || incidents.Count == 0)
                {
                    return NotFound("No incidents found.");
                }
                return Ok(incidents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }

    [Route("api/emergency")]
    [ApiController]
    public class EmergencyActionController : ControllerBase
    {
        private readonly IEmergencyActionService _emergencyActionService;

        public EmergencyActionController(IEmergencyActionService emergencyActionService)
        {
            _emergencyActionService = emergencyActionService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<EmergencyAction> emergencyActions = _emergencyActionService.GetAllEmergencyActions();
                if (emergencyActions == null || emergencyActions.Count == 0)
                {
                    return NotFound("No emergency actions found.");
                }
                return Ok(emergencyActions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

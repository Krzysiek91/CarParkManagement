using CarParkManagement.Api.Models.Requests;
using CarParkManagement.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarParkManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        private readonly ILogger<ParkingController> _logger;

        public ParkingController(ILogger<ParkingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<ParkVehicleResponse> ParkVehicle([FromBody] ParkVehicleReuqest request)
        {
            // TODO: implement, mocks for now
            
            var response = new ParkVehicleResponse
            {
                VehicleReg = request.VehicleReg,
                SpaceNumber = 1, 
                TimeIn = DateTime.Now
            };

            return Ok(response);
        }

        [HttpGet]
        public ActionResult<ParkingAvailabilityResponse> GetAvailability()
        {
            // TODO: implement, mocks for now
            var response = new ParkingAvailabilityResponse
            {
                AvailableSpaces = 95,
                OccupiedSpaces = 5
            };

            return Ok(response);
        }

        [HttpPost("exit")]
        public ActionResult<ExitVehicleResponse> ExitVehicle([FromBody] ExitVehicleRequest request)
        {
            // TODO: implement, mocks for now
            var response = new ExitVehicleResponse
            {
                VehicleReg = request.VehicleReg,
                VehicleCharge = 20.20,
                TimeIn = DateTime.Now.AddHours(-3),
                TimeOut = DateTime.Now
            };

            return Ok(response);
        }
    }
}

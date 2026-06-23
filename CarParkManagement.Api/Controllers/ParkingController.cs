using CarParkManagement.Api.Interfaces;
using CarParkManagement.Api.Models.Requests;
using CarParkManagement.Api.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarParkManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        // TODO : Add logging

        [HttpPost]
        public ActionResult<ParkVehicleResponse> ParkVehicle([FromBody] ParkVehicleReuqest request)
        {
            if (_parkingService.IsVehicleParked(request.VehicleReg))
            {
                return BadRequest($"Vehicle with registration {request.VehicleReg} is already parked.");
            }

            var result = _parkingService.ParkVehicle(request);

            if (result == null)
            {
                return Conflict("No available parking spaces.");
            }

            return Ok(result);
        }

        [HttpGet]
        public ActionResult<ParkingAvailabilityResponse> GetAvailability()
        {
            var result = _parkingService.GetAvailability();

            return Ok(result);
        }

        [HttpPost("exit")]
        public ActionResult<ExitVehicleResponse> ExitVehicle([FromBody] ExitVehicleRequest request)
        {
            var result = _parkingService.ExitVehicle(request);

            if (result == null)
            {
                return NotFound($"Vehicle with registration {request.VehicleReg} not found.");
            }

            return Ok(result);
    }
}}

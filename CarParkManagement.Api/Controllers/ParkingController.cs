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
        public void ParkVehicle()
        {
            throw new NotImplementedException();
        }
    }
}

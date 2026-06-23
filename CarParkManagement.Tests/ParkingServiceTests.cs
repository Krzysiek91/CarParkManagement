using CarParkManagement.Api.Interfaces;
using CarParkManagement.Api.Models;
using CarParkManagement.Api.Models.Requests;
using CarParkManagement.Api.Services;
using Moq;

namespace CarParkManagement.Tests
{
    public class ParkingServiceTests
    {
        // TODO : Add more tests to cover all scenarios in solution, edge cases etc.

        [Fact]
        public void ParkVehicle_WhenSpaceAvailable_ReturnsExpectedResponse()
        {
            //arrange
            var mockRepo = new Mock<IParkingRepository>();

            mockRepo.Setup(r => r.ParkVehicle("TEST1", VehicleType.Small, It.IsAny<DateTime>())).Returns(1);

            var service = new ParkingService(mockRepo.Object);
            var request = new ParkVehicleReuqest { VehicleReg = "TEST1", VehicleType = 1 };

            //act
            var result = service.ParkVehicle(request);

            //assert
            Assert.NotNull(result);
            Assert.Equal("TEST1", result.VehicleReg);
            Assert.Equal(1, result.SpaceNumber);
        }

        [Fact]
        public void ExitVehicle_SmallCar_CalculatesPriseCorrectly()
        {
            //arrange
            var mockRepo = new Mock<IParkingRepository>();
            var timeIn = DateTime.UtcNow.AddMinutes(-10);

            mockRepo.Setup(r => r.GetParkedVehicle("TEST1"))
                .Returns(new ParkingSpace
                {
                    VehicleReg = "TEST1",
                    VehicleType = VehicleType.Small,
                    TimeIn = timeIn
                });

            mockRepo.Setup(r => r.ExitVehicle("TEST1")).Returns(true);

            var service = new ParkingService(mockRepo.Object);
            var request = new ExitVehicleRequest { VehicleReg = "TEST1"};

            //act
            var ressult = service.ExitVehicle(request);

            //assert
            Assert.NotNull(ressult);
            // Expectation: 10 minutes * £0.10 = 1.00
            // 10/5 = 2, 2 * 1£ = £2.00
            // Total charge = £3.00

            Assert.Equal(3.00, ressult.VehicleCharge);
        }

        [Fact]
        public void ExitVehicle_WhenVehicleNotFound_ReturnsNull()
        {
            //arrange
            var mockRepo = new Mock<IParkingRepository>();
            mockRepo.Setup(r => r.GetParkedVehicle("TEST2")).Returns((ParkingSpace?)null);
            var service = new ParkingService(mockRepo.Object);
            var request = new ExitVehicleRequest { VehicleReg = "TEST2" };

            //act
            var result = service.ExitVehicle(request);
            //assert
            Assert.Null(result);
        }

        [Fact]
        public void GetAvailibility_ReturnsCorrectCounts()
        {
            //arrange
            var mockRepo = new Mock<IParkingRepository>();

            mockRepo.Setup(r => r.GetAvailableSpaces()).Returns(95);
            mockRepo.Setup(r => r.GetOccupiedSpaces()).Returns(5);

            var service = new ParkingService(mockRepo.Object);

            //act
            var result = service.GetAvailability();

            //assert
            Assert.Equal(95, result.AvailableSpaces);
            Assert.Equal(5, result.OccupiedSpaces);
        }
    }
}
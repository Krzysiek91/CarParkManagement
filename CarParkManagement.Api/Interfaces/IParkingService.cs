using CarParkManagement.Api.Models.Requests;
using CarParkManagement.Api.Models.Responses;

namespace CarParkManagement.Api.Interfaces;

public interface IParkingService
{
    ParkVehicleResponse? ParkVehicle(ParkVehicleReuqest request);
    ParkingAvailabilityResponse GetAvailability();
    ExitVehicleResponse? ExitVehicle(ExitVehicleRequest request);
    bool IsVehicleParked(string vehicleReg);
}

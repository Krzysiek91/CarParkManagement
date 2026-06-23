using CarParkManagement.Api.Models;

namespace CarParkManagement.Api.Interfaces;

public interface IParkingRepository
{
    // space allocation
    int? ParkVehicle(string vehicleReg, VehicleType vehicleType, DateTime timeIn);
    // info about the parked car, use for price calc
    ParkingSpace? GetParkedVehicle(string vehicleReg);
    // free the space
    bool ExitVehicle(string vehicleReg);
    int GetAvailableSpaces();
    int GetOccupiedSpaces();
}

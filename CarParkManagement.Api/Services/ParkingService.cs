using CarParkManagement.Api.Interfaces;
using CarParkManagement.Api.Models;
using CarParkManagement.Api.Models.Requests;
using CarParkManagement.Api.Models.Responses;

namespace CarParkManagement.Api.Services;

public class ParkingService : IParkingService
{
    private readonly IParkingRepository _repository;

    public ParkingService(IParkingRepository repository)
    {
        _repository = repository;
    }

    public ParkVehicleResponse? ParkVehicle(ParkVehicleReuqest request)
    {
        var entrenceTime = DateTime.UtcNow; // TODO: timeProvider for testing?
        var vehicleType = (VehicleType)request.VehicleType;

        var spaceNumber = _repository.ParkVehicle(request.VehicleReg, vehicleType, entrenceTime);

        if (spaceNumber == null)
            return null;

        return new ParkVehicleResponse
        {
            VehicleReg = request.VehicleReg,
            SpaceNumber = spaceNumber.Value,
            TimeIn = entrenceTime
        };
    }

    public ParkingAvailabilityResponse GetAvailability()
    {
        return new ParkingAvailabilityResponse
        {
            AvailableSpaces = _repository.GetAvailableSpaces(),
            OccupiedSpaces = _repository.GetAvailableSpaces()
        };
    }

    public bool IsVehicleParked(string vehicleReg)
    {
        return _repository.GetParkedVehicle(vehicleReg) != null;
    }

    public ExitVehicleResponse? ExitVehicle(ExitVehicleRequest request)
    {
        var parkedVehicle = _repository.GetParkedVehicle(request.VehicleReg);

        if (parkedVehicle == null)
            return null;

        var timeOut = DateTime.UtcNow; //TODO: Time provider for testing?
        var charge = CalculateCharge(parkedVehicle.VehicleType!.Value, parkedVehicle.TimeIn!.Value, timeOut);

        var success = _repository.ExitVehicle(request.VehicleReg);

        if (!success)
            return null;

        return new ExitVehicleResponse
        {
            VehicleReg = request.VehicleReg,
            VehicleCharge = charge,
            TimeIn = parkedVehicle.TimeIn.Value,
            TimeOut = timeOut
        };
    }

    private double CalculateCharge(VehicleType vehicleType, DateTime timeIn, DateTime timeout)
    {
        var totalMinutes = (int)(timeout - timeIn).TotalMinutes;

        // TODO: move 'magic numbers' to appsettings 
        var costPerMinute = vehicleType switch
        {
            VehicleType.Small => 0.10,
            VehicleType.Medium => 0.20,
            VehicleType.Large => 0.40,
            _ => 0.10
        };

        var initialCost = totalMinutes * costPerMinute;

        var extraCost = (totalMinutes / 5);

        var finalCost = initialCost + extraCost;

        return Math.Round(finalCost, 2);
    }
}

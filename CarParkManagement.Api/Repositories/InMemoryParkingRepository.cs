using CarParkManagement.Api.Interfaces;
using CarParkManagement.Api.Models;
using System.Collections.Concurrent;

namespace CarParkManagement.Api.Repositories;

public class InMemoryParkingRepository : IParkingRepository
{
    //TODO: To be replaced with a real database. Reuse the IParkingRepository interface
    private readonly ConcurrentDictionary<int, ParkingSpace> _parkingSpaces;
    private const int _toalSpaces = 100; // TODO : Make this configurable, move to appsettings.json

    public InMemoryParkingRepository()
    {
        _parkingSpaces = new ConcurrentDictionary<int, ParkingSpace>();
        InitializeSpaces();
    }

    public int? ParkVehicle(string vehicleReg, VehicleType vehicleType, DateTime timeIn)
    {
        // Check if vehicle already parked 
        if (_parkingSpaces.Values.Any(s => s.VehicleReg == vehicleReg))
        {
            return null;
        }

        // find the first available space
        var availableSpace = _parkingSpaces.Values
            .Where(s => !s.IsOccupied)
            .OrderBy(s => s.SpaceNumber)
            .FirstOrDefault();

        if (availableSpace == null)
        {
            return null;
        }

        var spacesNumber = availableSpace.SpaceNumber;

        _parkingSpaces[spacesNumber] = new ParkingSpace
        {
            SpaceNumber = availableSpace.SpaceNumber,
            IsOccupied = true,
            VehicleReg = vehicleReg,
            VehicleType = vehicleType,
            TimeIn = timeIn
        };

        return spacesNumber;
    }

    public ParkingSpace? GetParkedVehicle(string vehicleReg)
    {
        return _parkingSpaces.Values.FirstOrDefault(s => s.VehicleReg == vehicleReg);
    }

    public bool ExitVehicle(string vehicleReg)
    {
        var parkedSpace = GetParkedVehicle(vehicleReg);
        if (parkedSpace == null)
            return false;

        var spaceNumber = parkedSpace.SpaceNumber;

        var updatedSpace = _parkingSpaces[spaceNumber] = new ParkingSpace
        {
            SpaceNumber = parkedSpace.SpaceNumber,
            IsOccupied = false,
            VehicleReg = null,
            VehicleType = null,
            TimeIn = null
        };

        return true;
    }

    public int GetAvailableSpaces()
    {
        return _parkingSpaces.Values.Count(s => !s.IsOccupied);
    }
    public int GetOccupiedSpaces()
    {
        return _parkingSpaces.Values.Count(s => s.IsOccupied);
    }

    // Make all spaces available at start
    private void InitializeSpaces()
    {
        for (int i = 1; i <= _toalSpaces; i++)
        {
            _parkingSpaces.TryAdd(i, new ParkingSpace
            {
                SpaceNumber = i,
                IsOccupied = false
            });
        }
    }
}

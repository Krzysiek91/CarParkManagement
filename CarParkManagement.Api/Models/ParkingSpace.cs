namespace CarParkManagement.Api.Models;

public class ParkingSpace
{
    public int SpaceNumbre { get; set; }
    public bool IsOccupied { get; set; }
    public required string VehicleReg {  get; set; }
    public required VehicleType VehicleType { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace CarParkManagement.Api.Models.Requests;

public class ParkVehicleReuqest
{
    [Required]
    public required string VehicleReg { get; set;}

    [Required]
    public int VehicleType { get; set; }
}

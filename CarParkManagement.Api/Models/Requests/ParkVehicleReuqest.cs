using System.ComponentModel.DataAnnotations;

namespace CarParkManagement.Api.Models.Requests;

public class ParkVehicleReuqest
{
    [Required]
    public required string VehicleReg { get; set;}

    [Required]
    [Range(1, 3)]
    public int VehicleType { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace CarParkManagement.Api.Models.Requests;

public class ExitVehicleRequest
{
    [Required]
    public required string VehicleReg {  get; set; }
}


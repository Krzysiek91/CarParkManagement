namespace CarParkManagement.Api.Models.Responses;

public class ExitVehicleResponse
{
    public string VehicleReg { get; set; }
    // I want to match the requirements contract exactly, but peronsally I would use decimal for money
    public double VehicleCharge { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime TimeOut { get; set; }
}

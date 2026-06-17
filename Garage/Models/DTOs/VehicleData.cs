namespace Garage.Models.DTOs;

public class VehicleData
{
    public string VehicleType { get; set; } = "";
    public string RegistrationNumber { get; set; } = "";
    public string Color { get; set; } = "";
    public int Wheels { get; set; }

    // Car
    public string? FuelType { get; set; }

    // Motorcycle
    public int? CylinderVolume { get; set; }

    // Bus
    public int? NumberOfSeats { get; set; }

    // Boat
    public double? Length { get; set; }

    // Airplane
    public int? NumberOfEngines { get; set; }
}
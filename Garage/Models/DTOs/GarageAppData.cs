using Garage.Models.Vehicles;

namespace Garage.Models.DTOs;

public class GarageAppData
{
    public string? CurrentGarageName { get; set; }
    public List<GarageData> Garages { get; set; } = new();
}
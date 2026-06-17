using Garage.Models.Vehicles;

namespace Garage.Models.DTOs;

public class GarageData
{
    public string Name { get; set; } = "";
    public int Capacity { get; set; } // Garage total spots
    public int Count { get; set; } // Number of Vehicles in Garage
    public List<VehicleData> Vehicles { get; set; } = new();
}
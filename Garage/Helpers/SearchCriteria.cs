using Garage.Models.Enums;

namespace Garage.Helpers;

public class SearchCriteria
{
    public List<VehicleType> VehicleTypes { get; set; } = [];
    public string[]? Colors { get; set; } = [];
    public int[]? Wheels { get; set; } = [];
    public List<FuelType> FuelTypes { get; set; } = [];
    public int[]? CylinderVolumes { get; set; } = [];
    public int[]? NumbersOfSeats { get; set; } = [];
    public double[]? Lengths { get; set; } = [];
    public int[]? NumbersOfEngines { get; set; } = [];
}
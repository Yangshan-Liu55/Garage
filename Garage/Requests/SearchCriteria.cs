using Garage.Enums;

namespace Garage.Requests;

public class SearchCriteria
{
    public List<VehicleType> VehicleTypes { get; set; } = [];
    public string[]? Colors { get; set; } = [];
    public int[]? Wheels { get; set; } = [];
}
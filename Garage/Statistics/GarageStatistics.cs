namespace Garage.Statistics;

public class GarageStatistics
{
    public int TotalParkedVehicles { get; set; }

    public int Capacity { get; set; }

    public Dictionary<string, int> CountsByVehicleTypes { get; set; } = [];
}
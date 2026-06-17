using Garage.Models.Vehicles;
using Garage.Models.Statistics;
using Garage.Helpers;

namespace Garage.Interfaces;

public interface IGarageHandler
{
    bool InitializeGarage();

    bool AddVehicle(Vehicle vehicle);
    bool CheckRegistrationNumber(String regNumber);
    bool RemoveVehicle(String regNumber);

    Vehicle? FindVehicle(String regNumber);

    IEnumerable<Vehicle>? SearchVehicles(SearchCriteria criteria);

    IEnumerable<Vehicle>? GetAllVehicles();

    // List vehicle types and how many of each are in the garage
    GarageStatistics? GetGarageStatistics();
}
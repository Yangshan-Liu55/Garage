using Garage.Vehicles;
using Garage.Statistics;
using Garage.Requests;

namespace Garage.Interfaces;

internal interface IGarageHandler
{
    bool CreateGarage(int capacity);
    IEnumerable<Vehicle> GetAllVehicles();
    GarageStatistics GetGarageStatistics(); // List vehicle types and how many of each are in the garage
    bool InitializeGarage();

    bool AddVehicle(Vehicle vehicle);
    bool CheckRegistrationNumber(String regNumber);
    bool RemoveVehicle(String regNumber);
    Vehicle FindVehicle(String regNumber);
    IEnumerable<Vehicle> SearchVehicles(SearchCriteria criteria);
}
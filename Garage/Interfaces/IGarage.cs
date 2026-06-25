using Garage.Models.Enums;
using Garage.Models.Vehicles;

namespace Garage.Interfaces;

public interface IGarage
{
    string Name { get; }
    int Capacity { get; }
    int Count { get; }

    VehicleType VehicleType { get; }

    double AvailableSpace { get; }
    double OccupiedSpace { get; }

    bool IsRegNumberContained(string regNumber);
    bool Retrieve(string regNumber);

}
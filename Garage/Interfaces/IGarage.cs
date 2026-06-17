using Garage.Models.Vehicles;

namespace Garage.Interfaces;

public interface IGarage : IEnumerable<Vehicle>
{
    string Name { get; }
    int Capacity { get; }
    int Count { get; }

    double AvailableSpace { get; }
    double OccupiedSpace { get; }

    bool Park(Vehicle vehicle);

    bool IsRegNumberContained(string regNumber);

    bool Retrieve(string regNumber);

    Vehicle? FindByRegNumber(string regNumber);
}
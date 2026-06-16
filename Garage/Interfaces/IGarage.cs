using Garage.Models.Vehicles;

namespace Garage.Interfaces;

internal interface IGarage : IEnumerable<Vehicle>
{
    string Name { get; }
    int Capacity { get; }
    int Count { get; }

    bool Park(Vehicle vehicle);

    bool IsRegNumberContained(string regNumber);

    bool Retrieve(string regNumber);

    Vehicle? FindByRegNumber(string regNumber);
}
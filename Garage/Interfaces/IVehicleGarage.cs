using Garage.Models.Vehicles;

namespace Garage.Interfaces;

public interface IVehicleGarage<T> : IGarage, IEnumerable<T> where T : Vehicle
{
    bool Park(T vehicle);

    T? FindByRegNumber(string regNumber);
}
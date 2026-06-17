

namespace Garage.Interfaces;

public interface IVehicle
{
    string RegistrationNumber { get;}
    string Color { get; }
    int Wheels { get; }
}
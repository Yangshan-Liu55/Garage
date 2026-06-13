

namespace Garage.Interfaces;

internal interface IVehicle
{
    string RegistrationNumber { get;}
    string Color { get; }
    int Wheels { get; }
}
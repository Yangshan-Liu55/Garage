using Garage.Interfaces;
using Garage.Models.Enums;

namespace Garage.Models.Vehicles;

public abstract class Vehicle : IVehicle
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public int Wheels { get; set; }
    public abstract VehicleType VehicleType { get; }
    public abstract double RequiredSpace { get; }

    protected Vehicle(string registrationNumber, string color, int wheels)
    {
        RegistrationNumber = registrationNumber;
        Color = color;
        Wheels = wheels;
    }

    public override string ToString()
    {
        return $"Registration number: {RegistrationNumber}, type: {GetType().Name}, color: {Color}, wheels: {Wheels}";
    }
}
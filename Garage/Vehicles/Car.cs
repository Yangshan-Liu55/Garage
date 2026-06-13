using Garage.Enums;

namespace Garage.Vehicles;

public class Car : Vehicle
{
    public FuelType FuelType { get; set; }
    public override VehicleType VehicleType => VehicleType.Car;

    public Car(string registrationNumber, string color, int wheels, FuelType fuelType)
        : base(registrationNumber, color, wheels)
    {
        FuelType = fuelType;
    }

    public override string ToString()
    {
        return base.ToString() + $", fuel type: {FuelType}";
    }
}
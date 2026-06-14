using Garage.Enums;

namespace Garage.Vehicles;

public class Motorcycle : Vehicle
{
    public int CylinderVolum { get; set; }
    public override VehicleType VehicleType => VehicleType.Motorcycle;

    public Motorcycle(string registrationNumber, string color, int wheels, int cylinderVolum)
        : base(registrationNumber, color, wheels)
    {
        CylinderVolum = cylinderVolum;
    }

    public override string ToString()
    {
        return base.ToString() + $", cylinder volum: {CylinderVolum} cc";
    }
}
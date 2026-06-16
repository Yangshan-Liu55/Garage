using Garage.Models.Enums;

namespace Garage.Models.Vehicles;

public class Motorcycle : Vehicle
{
    public int CylinderVolume { get; set; }
    public override VehicleType VehicleType => VehicleType.Motorcycle;

    public Motorcycle(string registrationNumber, string color, int wheels, int cylinderVolume)
        : base(registrationNumber, color, wheels)
    {
        CylinderVolume = cylinderVolume;
    }

    public override string ToString()
    {
        return base.ToString() + $", cylinder volum: {CylinderVolume} cc";
    }
}
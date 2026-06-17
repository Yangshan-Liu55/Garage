using Garage.Helpers;
using Garage.Models.Enums;

namespace Garage.Models.Vehicles;

public class Boat : Vehicle
{
    public double Length { get; set; }
    public override VehicleType VehicleType => VehicleType.Boat;
    public override double RequiredSpace =>
        GarageConstants.VehicleRequiredSpace[VehicleType.Boat];

    public Boat(string registrationNumber, string color, int wheels, double length)
        : base(registrationNumber, color, wheels)
    {
        Length = length;
    }

    public override string ToString()
    {
        return base.ToString() + $", length: {Length} m";
    }
}
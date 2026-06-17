using Garage.Helpers;
using Garage.Models.Enums;

namespace Garage.Models.Vehicles;

public class Airplane : Vehicle
{
    public int NumberOfEngines { get; set; }
    public override VehicleType VehicleType => VehicleType.Airplane;
    public override double RequiredSpace =>
        GarageConstants.VehicleRequiredSpace[VehicleType.Airplane];

    public Airplane(string registrationNumber, string color, int wheels, int numberOfEngines)
        : base(registrationNumber, color, wheels)
    {
        NumberOfEngines = numberOfEngines;
    }

    public override string ToString()
    {
        return base.ToString() + $", number of engines: {NumberOfEngines}";
    }
}
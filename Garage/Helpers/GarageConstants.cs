using Garage.Models.Enums;

namespace Garage.Helpers;

public static class GarageConstants
{
    public static readonly Dictionary<VehicleType, double> VehicleRequiredSpace = new (){
        {VehicleType.Car, 1},
        {VehicleType.Motorcycle, 0.33},
        {VehicleType.Bus, 1.5},
        {VehicleType.Boat, 2},
        {VehicleType.Airplane, 3},
    };
}
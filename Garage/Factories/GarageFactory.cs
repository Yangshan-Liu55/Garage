using Garage.Interfaces;
using Garage.Models;
using Garage.Models.Vehicles;
using Garage.Models.Enums;

namespace Garage.Factories;

public static class GarageFactory
{
    public static IGarage CreateGarage(string name, int capacity, VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.Vehicle => new Garage<Vehicle>(name, capacity, vehicleType),

            VehicleType.Car => new Garage<Car>(name, capacity, vehicleType),
            VehicleType.Bus => new Garage<Bus>(name, capacity, vehicleType),
            VehicleType.Motorcycle => new Garage<Motorcycle>(name, capacity, vehicleType),
            VehicleType.Boat => new Garage<Boat>(name, capacity, vehicleType),
            VehicleType.Airplane => new Garage<Airplane>(name, capacity, vehicleType),

            _ => throw new ArgumentException($"Unknown vehicle type")
        };
    }
}
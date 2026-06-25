using Garage.Models.Enums;
using Garage.Interfaces;
using Garage.Models.Vehicles;
using Garage.Helpers;

namespace Garage.Factories;

public abstract class VehicleFactory
{
    public static bool Park(IGarage? garage, Vehicle vehicle)
    {
        if (!(Enum.Equals(garage?.VehicleType, VehicleType.Vehicle)
                || Enum.Equals(garage?.VehicleType, vehicle.VehicleType))
            && vehicle.RequiredSpace <= garage?.AvailableSpace) // (_count >= _capacity)
        {
            return false;
        }
        switch (garage?.VehicleType)
        {
            case VehicleType.Vehicle:
                return ((IVehicleGarage<Vehicle>)garage).Park(vehicle);
            case VehicleType.Car:
                return ((IVehicleGarage<Car>)garage).Park((Car)vehicle);
            case VehicleType.Motorcycle:
                return ((IVehicleGarage<Motorcycle>)garage).Park((Motorcycle)vehicle);
            case VehicleType.Bus:
                return ((IVehicleGarage<Bus>)garage).Park((Bus)vehicle);
            case VehicleType.Boat:
                return ((IVehicleGarage<Boat>)garage).Park((Boat)vehicle);
            case VehicleType.Airplane:
                return ((IVehicleGarage<Airplane>)garage).Park((Airplane)vehicle);
            default:
                return false;
        }
    }

    public static Vehicle? FindByRegNumber(IGarage? garage, string regNumber)
    {
        switch (garage?.VehicleType)
        {
            case VehicleType.Vehicle:
                return ((IVehicleGarage<Vehicle>)garage).FindByRegNumber(regNumber);
            case VehicleType.Car:
                return ((IVehicleGarage<Car>)garage).FindByRegNumber(regNumber);
            case VehicleType.Motorcycle:
                return ((IVehicleGarage<Motorcycle>)garage).FindByRegNumber(regNumber);
            case VehicleType.Bus:
                return ((IVehicleGarage<Bus>)garage).FindByRegNumber(regNumber);
            case VehicleType.Boat:
                return ((IVehicleGarage<Boat>)garage).FindByRegNumber(regNumber);
            case VehicleType.Airplane:
                return ((IVehicleGarage<Airplane>)garage).FindByRegNumber(regNumber);
            default:
                return null;
        }
    }

    public static IEnumerable<Vehicle>? GetAllVehicles(IGarage? garage)
    {
        switch (garage?.VehicleType)
        {
            case VehicleType.Vehicle:
                return ((IVehicleGarage<Vehicle>)garage);
            case VehicleType.Car:
                return ((IVehicleGarage<Car>)garage);
            case VehicleType.Motorcycle:
                return ((IVehicleGarage<Motorcycle>)garage);
            case VehicleType.Bus:
                return ((IVehicleGarage<Bus>)garage);
            case VehicleType.Boat:
                return ((IVehicleGarage<Boat>)garage);
            case VehicleType.Airplane:
                return ((IVehicleGarage<Airplane>)garage);
            default:
                return null;
        }
    }

}

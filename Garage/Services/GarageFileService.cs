using System.Text.Json;

using Garage.Models.DTOs;
using Garage.Models.Vehicles;
using Garage.Interfaces;
using Garage.Managers;
using Garage.Models.Enums;
using Garage.Handlers;
using Garage.Factories;

namespace Garage.Services;

public static class GarageFileService
{
    public static void SaveGarages(string filePath, 
        GarageManager garageManager)
    {
        GarageAppData data = new();

        data.CurrentGarageName = garageManager.CurrentGarageName;

        foreach (IGarage garage in garageManager.GetAllGarages())
        {
            GarageData garageData = new()
            {
                Name = garage.Name,
                Capacity = garage.Capacity,
                Count = garage.Count,
                VehicleType = garage.VehicleType,
                Vehicles = VehicleFactory.GetAllVehicles(garage)?
                    .Select(v => ConvertVehicle(v)).ToList()
                    ?? []
            };

            data.Garages.Add(garageData);
        }

        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(data, options);

        File.WriteAllText(filePath, json);
    }

    public static GarageAppData? LoadGarages(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        string json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<GarageAppData>(json);
    }

    private static VehicleData ConvertVehicle(Vehicle vehicle)
    {
        VehicleData data = new()
        {
            VehicleType = vehicle.GetType().Name,

            RegistrationNumber = vehicle.RegistrationNumber,

            Color = vehicle.Color,

            Wheels = vehicle.Wheels
        };

        switch (vehicle)
        {
            case Car car:
                data.FuelType = car.FuelType.ToString();
                break;

            case Motorcycle mc:
                data.CylinderVolume = mc.CylinderVolume;
                break;

            case Bus bus:
                data.NumberOfSeats = bus.NumberOfSeats;
                break;

            case Boat boat:
                data.Length = boat.Length;
                break;

            case Airplane airplane:
                data.NumberOfEngines = airplane.NumberOfEngines;
                break;
        }

        return data;
    }

    public static Vehicle? ConvertVehicleData(VehicleData data)
    {
        switch (data.VehicleType)
        {
            case nameof(Car):
                return new Car(
                        data.RegistrationNumber,
                        data.Color,
                        data.Wheels,
                        Enum.Parse<FuelType>(data.FuelType!)
                    );
            case nameof(Motorcycle):
                return new Motorcycle(
                        data.RegistrationNumber,
                        data.Color,
                        data.Wheels,
                        data.CylinderVolume ?? 0
                    );
            case nameof(Bus):
                return new Bus(
                    data.RegistrationNumber,
                    data.Color,
                    data.Wheels,
                    data.NumberOfSeats ?? 0
                );
            case nameof(Boat):
                return new Boat(
                    data.RegistrationNumber,
                    data.Color,
                    data.Wheels,
                    data.Length ?? 0
                );
            case nameof(Airplane):
                return new Airplane(
                    data.RegistrationNumber,
                    data.Color,
                    data.Wheels,
                    data.NumberOfEngines ?? 0
                );
            default:
                return null;
        }
    }

}
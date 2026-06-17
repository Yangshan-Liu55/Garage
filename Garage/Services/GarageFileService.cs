using System.Text.Json;
using Garage.Models.DTOs;
using Garage.Models.Vehicles;
using Garage.Interfaces;
using Garage.Managers;
using Garage.Models.Enums;

namespace Garage.Services;

public static class GarageFileService
{
    public static void SaveGarages(string filePath, GarageManager garageManager)
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
                Vehicles = garage.Select(ConvertVehicle).ToList()
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

}
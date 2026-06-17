using Garage.Interfaces;
using Garage.Models.Vehicles;
using Garage.Models.Enums;
using Garage.Models.Statistics;
using Garage.Helpers;
using Garage.Managers;
using System.Text;
using System.Linq;
using Garage.Services;
using Garage.Models.DTOs;
using Garage.Models;

namespace Garage.Handlers;

public class GarageHandler : IGarageHandler
{
    private readonly GarageManager _garageManager;

    private IGarage? CurrentGarage => _garageManager?.GetCurrentGarage();

    public double CurrentAvailableSpace => CurrentGarage?.AvailableSpace ?? 0;

    public GarageHandler(GarageManager garageManager)
    {
        _garageManager = garageManager;
    }

    public bool AddVehicle(Vehicle vehicle)
    {
        IGarage? garage = CurrentGarage;
        if (garage is null)
        {
            return false;
        }
        return garage.AvailableSpace >= vehicle.RequiredSpace
            && garage.Park(vehicle);
    }

    public bool CheckRegistrationNumber(string regNumber)
    {
        return CurrentGarage?.IsRegNumberContained(regNumber) ?? false;
    }

    public bool RemoveVehicle(string regNumber)
    {
        return CurrentGarage?.Retrieve(regNumber) ?? false;
    }

    public Vehicle? FindVehicle(string regNumber)
    {
        return CurrentGarage?.FindByRegNumber(regNumber) ?? null;
    }

    public IEnumerable<Vehicle>? SearchVehicles(SearchCriteria criteria)
    {
        if (CurrentGarage is null || CurrentGarage.Count < 1) { return null; }

        IEnumerable<Vehicle> query = CurrentGarage;

        if (criteria.VehicleTypes.Count > 0)
        {
            query = query.Where(v => criteria.VehicleTypes.Contains(v.VehicleType));
        }

        if (criteria.Colors != null && criteria.Colors.Length > 0)
        {
            query = query.Where(v => Array.Exists(criteria.Colors,
                element => element.Equals(v.Color, StringComparison.OrdinalIgnoreCase)));
        }

        if (criteria.Wheels != null && criteria.Wheels.Length > 0)
        {
            query = query.Where(v => Array.IndexOf(criteria.Wheels, v.Wheels) > -1);
        }

        if (criteria.FuelTypes.Count > 0)
        {
            query = query.Where(v =>
            {
                if (v.VehicleType.Equals(VehicleType.Car))
                {
                    Car car = (Car)v;
                    return criteria.FuelTypes.Contains(car.FuelType);
                }
                return false;

            });
        }

        if (criteria.CylinderVolumes != null && criteria.CylinderVolumes.Length > 0)
        {
            query = query.Where(v =>
            {
                if (v.VehicleType.Equals(VehicleType.Motorcycle))
                {
                    Motorcycle m = (Motorcycle)v;
                    return Array.IndexOf(criteria.CylinderVolumes, m.CylinderVolume) > -1;
                }
                return false;
            });
        }

        if (criteria.NumbersOfSeats != null && criteria.NumbersOfSeats.Length > 0)
        {
            query = query.Where(v =>
            {
                if (v.VehicleType.Equals(VehicleType.Bus))
                {
                    Bus b = (Bus)v;
                    return Array.IndexOf(criteria.NumbersOfSeats, b.NumberOfSeats) > -1;
                }
                return false;
            });
        }

        if (criteria.Lengths != null && criteria.Lengths.Length > 0)
        {
            query = query.Where(v =>
            {
                if (v.VehicleType.Equals(VehicleType.Boat))
                {
                    Boat b = (Boat)v;
                    return Array.IndexOf(criteria.Lengths, b.Length) > -1;
                }
                return false;
            });
        }

        if (criteria.NumbersOfEngines != null && criteria.NumbersOfEngines.Length > 0)
        {
            query = query.Where(v =>
            {
                if (v.VehicleType.Equals(VehicleType.Airplane))
                {
                    Airplane a = (Airplane)v;
                    return Array.IndexOf(criteria.NumbersOfEngines, a.NumberOfEngines) > -1;
                }
                return false;
            });
        }

        return query;
    }

    public IEnumerable<Vehicle>? GetAllVehicles()
    {
        return CurrentGarage;
    }

    public GarageStatistics? GetGarageStatistics()
    {
        if (CurrentGarage == null || CurrentGarage.Count < 1)
        {
            return null;
        }

        /* var query = CurrentGarage.GroupBy(
            v => v.GetType().Name,
            v => v.RegistrationNumber,
            (vehicleType, vehicleGroup) => new
            {
                Key = vehicleType,
                Count = vehicleGroup.Count()
            }
        );

        foreach (var group in query)
        {
            sb.AppendLine($"{group.Key}: {group.Count}");
        } */

        /* var query = CurrentGarage.GroupBy(v => v.GetType().Name)
                                    .Select(g => new
                                    {
                                        Type = g.Key,
                                        Count = g.Count()
                                    }); */

        return new GarageStatistics
        {
            TotalParkedVehicles = CurrentGarage.Count,
            Capacity = CurrentGarage.Capacity,

            CountsByVehicleTypes = CurrentGarage
            .GroupBy(v => v.GetType().Name)
            .ToDictionary(
                g => g.Key,
                g => g.Count())
        };
    }

    public int GetCapacity()
    {
        return CurrentGarage?.Capacity ?? 0;
    }

    public int GetCount()
    {
        return CurrentGarage?.Count ?? 0;
    }

    // Populate some vehicles to the current garage
    public bool InitializeGarage()
    {
        if (CurrentGarage == null)
        {
            return false;
        }

        for (int i = 0; i < 5; i++)
        {
            string regNumber = GenerateRandomRegNumber();
            string color = InputHelpers.GetRandomColor();
            FuelType fuelType = InputHelpers.GetRandomFuelType();

            Car car = new Car(regNumber, color, 4, fuelType);
            CurrentGarage.Park(car);
        }

        for (int i = 0; i < 5; i++)
        {
            string regNumber = GenerateRandomRegNumber();
            string color = InputHelpers.GetRandomColor();

            Motorcycle vehicle = new Motorcycle(regNumber, color, 2, 100 * (i + 1));
            CurrentGarage.Park(vehicle);
        }

        for (int i = 0; i < 5; i++)
        {
            string regNumber = GenerateRandomRegNumber();
            string color = InputHelpers.GetRandomColor();

            Bus vehicle = new Bus(regNumber, color, 4, 10 * (i + 1));
            CurrentGarage.Park(vehicle);
        }

        for (int i = 0; i < 5; i++)
        {
            string regNumber = GenerateRandomRegNumber();
            string color = InputHelpers.GetRandomColor();

            Boat vehicle = new Boat(regNumber, color, 0, 5 + i);
            CurrentGarage.Park(vehicle);
        }

        for (int i = 0; i < 5; i++)
        {
            string regNumber = GenerateRandomRegNumber();
            string color = InputHelpers.GetRandomColor();
            int engines = (i + 1) <= 4 ? (i + 1) : 1;

            Airplane vehicle = new Airplane(regNumber, color, 3, engines);
            CurrentGarage.Park(vehicle);
        }

        return true;
    }

    public bool SaveSystem(string filePath)
    {
        GarageFileService.SaveGarages(filePath, _garageManager);

        return true;
    }

    public bool LoadSystem(string filePath)
    {
        var systemData = GarageFileService.LoadGarages(filePath);

        if (systemData is null)
        {
            return false;
        }

        _garageManager.Clear();

        foreach (GarageData garageData in systemData.Garages)
        {
            IGarage garage = new Garage<Vehicle>(garageData.Name, garageData.Capacity);

            foreach (VehicleData vehicleData in garageData.Vehicles)
            {
                Vehicle? vehicle = CreateVehicle(vehicleData);
                if (vehicle is not null)
                {
                    garage.Park(vehicle);
                }
            }

            _garageManager.AddGarage(garage);
        }

        if (!string.IsNullOrWhiteSpace(
            systemData.CurrentGarageName))
        {
            _garageManager.SelectGarage(
                systemData.CurrentGarageName);
        }

        return true;
    }

    private string GenerateRandomRegNumber()
    {
        string regNumber;
        do
        {
            regNumber = InputHelpers.GenerateRandomUpperAlphanumericString(6);
            if (!CheckRegistrationNumber(regNumber))
            {
                return regNumber;
            }
        } while (true);
    }

    private Vehicle? CreateVehicle(VehicleData data)
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
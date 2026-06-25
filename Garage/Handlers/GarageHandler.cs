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
using Garage.Factories;

namespace Garage.Handlers;

public class GarageHandler : IGarageHandler
{
    private readonly GarageManager _garageManager;

    private IGarage? _garage => _garageManager?.CurrentGarage;

    public double CurrentOccupiedSpace => _garage?.OccupiedSpace ?? 0;

    public double CurrentAvailableSpace => (_garage?.Capacity - CurrentOccupiedSpace) ?? 0;

    public GarageHandler(GarageManager garageManager)
    {
        _garageManager = garageManager;
    }

    public bool AddVehicle(Vehicle vehicle)
    {
        return VehicleFactory.Park(_garage, vehicle);
    }

    public bool CheckRegistrationNumber(string regNumber)
    {
        return _garage?.IsRegNumberContained(regNumber) ?? false;
    }

    public bool RemoveVehicle(string regNumber)
    {
        return _garage?.Retrieve(regNumber) ?? false;
    }

    public Vehicle? FindVehicle(string regNumber)
    {
        return VehicleFactory.FindByRegNumber(_garage, regNumber);
    }

    public IEnumerable<Vehicle>? SearchVehicles(SearchCriteria criteria)
    {
        IEnumerable<Vehicle>? query = VehicleFactory.GetAllVehicles(_garage);

        if (query is null) { return null; }

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
        return VehicleFactory.GetAllVehicles(_garage);
    }

    public GarageStatistics? GetGarageStatistics()
    {
        IEnumerable<Vehicle>? query = VehicleFactory.GetAllVehicles(_garage);

        /* var query = _garage.GroupBy(
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

        /* var query = _garage.GroupBy(v => v.GetType().Name)
                                    .Select(g => new
                                    {
                                        Type = g.Key,
                                        Count = g.Count()
                                    }); */

        if (query is null) { return null; }
        return new GarageStatistics
        {
            TotalParkedVehicles = _garage?.Count ?? 0,
            Capacity = _garage?.Capacity ?? 0,

            CountsByVehicleTypes = query
            .GroupBy(v => v.GetType().Name)
            .ToDictionary(
                g => g.Key,
                g => g.Count())
        };
    }

    // Populate some vehicles to the current garage
    public bool InitializeGarage()
    {
        if (_garage is null)
        {
            return false;
        }

        if (_garage.VehicleType == VehicleType.Vehicle || _garage.VehicleType == VehicleType.Car)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GarageConstants.VehicleRequiredSpace[VehicleType.Car]
                    > _garage.AvailableSpace)
                {
                    break;
                }

                string regNumber = GenerateRandomRegNumber();
                string color = InputHelpers.GetRandomColor();

                FuelType fuelType = InputHelpers.GetRandomFuelType();

                Car car = new Car(regNumber, color, 4, fuelType);

                VehicleFactory.Park(_garage, car);
            }
        }

        if (_garage.VehicleType == VehicleType.Vehicle || _garage.VehicleType == VehicleType.Motorcycle)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GarageConstants.VehicleRequiredSpace[VehicleType.Motorcycle]
                    > _garage.AvailableSpace)
                {
                    break;
                }

                string regNumber = GenerateRandomRegNumber();
                string color = InputHelpers.GetRandomColor();

                Motorcycle vehicle = new Motorcycle(regNumber, color, 2, 100 * (i + 1));

                VehicleFactory.Park(_garage, vehicle);
            }
        }

        if (_garage.VehicleType == VehicleType.Vehicle || _garage.VehicleType == VehicleType.Bus)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GarageConstants.VehicleRequiredSpace[VehicleType.Bus]
                    > _garage.AvailableSpace)
                {
                    break;
                }

                string regNumber = GenerateRandomRegNumber();
                string color = InputHelpers.GetRandomColor();

                Bus vehicle = new Bus(regNumber, color, 4, 10 * (i + 1));

                VehicleFactory.Park(_garage, vehicle);
            }
        }


        if (_garage.VehicleType == VehicleType.Vehicle || _garage.VehicleType == VehicleType.Boat)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GarageConstants.VehicleRequiredSpace[VehicleType.Boat]
                    > _garage.AvailableSpace)
                {
                    break;
                }

                string regNumber = GenerateRandomRegNumber();
                string color = InputHelpers.GetRandomColor();

                Boat vehicle = new Boat(regNumber, color, 0, 5 + i);

                VehicleFactory.Park(_garage, vehicle);
            }
        }

        if (_garage.VehicleType == VehicleType.Vehicle || _garage.VehicleType == VehicleType.Airplane)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GarageConstants.VehicleRequiredSpace[VehicleType.Airplane]
                    > _garage.AvailableSpace)
                {
                    break;
                }

                string regNumber = GenerateRandomRegNumber();
                string color = InputHelpers.GetRandomColor();
                int engines = (i + 1) <= 4 ? (i + 1) : 1;

                Airplane vehicle = new Airplane(regNumber, color, 3, engines);

                VehicleFactory.Park(_garage, vehicle);
            }
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
            IGarage garage = GarageFactory.CreateGarage(garageData.Name, garageData.Capacity, garageData.VehicleType);

            foreach (VehicleData vehicleData in garageData.Vehicles)
            {
                Vehicle? vehicle = GarageFileService.ConvertVehicleData(vehicleData);
                if (vehicle is not null)
                {
                    VehicleFactory.Park(garage, vehicle);
                }
            }

            _garageManager.AddGarage(garage);
        }

        if (!string.IsNullOrWhiteSpace(systemData.CurrentGarageName))
        {
            _garageManager.SelectGarage(systemData.CurrentGarageName);
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

}
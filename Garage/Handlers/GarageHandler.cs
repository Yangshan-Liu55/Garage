using Garage.Interfaces;
using Garage.Vehicles;
using Garage.Helpers;
using Garage.Enums;
using Garage.Statistics;
using Garage.Requests;
using System.Text;
using System.Linq;

namespace Garage.Handlers;

internal class GarageHandler : IGarageHandler
{
    private Garage<Vehicle>? _garage;

    public bool CreateGarage(int capacity)
    {
        _garage = new Garage<Vehicle>(capacity);
        return true;
    }

    public bool AddVehicle(Vehicle vehicle)
    {
        if (_garage is null) { return false; }

        return _garage.Park(vehicle);
    }

    public bool CheckRegistrationNumber(String regNumber)
    {
        if (_garage is null) { return false; }

        return _garage.IsRegNumberContained(regNumber);
    }

    public bool RemoveVehicle(String regNumber)
    {
        if (_garage is null) { return false; }

        return _garage.Retrieve(regNumber);
    }

    public Vehicle? FindVehicle(String regNumber)
    {
        if (_garage is null) { return null; }

        return _garage.FindByRegNumber(regNumber);
    }

    public IEnumerable<Vehicle>? SearchVehicles(SearchCriteria criteria)
    {
        if (_garage is null) { return null; }

        IEnumerable<Vehicle> query = _garage;

        if (_garage == null || _garage.Count < 1)
        {
            return null;
        }

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

        return query;
    }

    public IEnumerable<Vehicle>? GetAllVehicles()
    {
        if (_garage is null) { return null; }

        return _garage;
    }

    public GarageStatistics? GetGarageStatistics()
    {
        if (_garage == null || _garage.Count < 1)
        {
            return null;
        }

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

        return new GarageStatistics
        {
            TotalParkedVehicles = _garage.Count,
            Capacity = _garage.Capacity,

            CountsByVehicleTypes = _garage
            .GroupBy(v => v.GetType().Name)
            .ToDictionary(
                g => g.Key,
                g => g.Count())
        };
    }

    public int GetCapacity()
    {
        if (_garage is null) { return 0; }

        return _garage.Capacity;
    }

    public int GetCount()
    {
        if (_garage is null) { return 0; }

        return _garage.Count;
    }

    // Populate some vehicles to the garage
    public bool InitializeGarage()
    {
        if (_garage == null)
        {
            _garage = new Garage<Vehicle>(100);
        }

        for (int i = 0; i < 5; i++)
        {
            string regNumber = GenerateRandomRegNumber();
            string color = InputHelpers.GetRandomColor();
            FuelType fuelType = InputHelpers.GetRandomFuelType();

            Car car = new Car(regNumber, color, 4, fuelType);
            _garage.Park(car);
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
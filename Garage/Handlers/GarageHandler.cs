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
    private Garage<Vehicle> _garage;

    public bool CreateGarage(int capacity)
    {
        _garage = new Garage<Vehicle>(capacity);
        return true;
    }

    public bool AddVehicle(Vehicle vehicle)
    {
        return _garage.Park(vehicle);
    }

    public bool CheckRegistrationNumber(String regNumber)
    {
        return _garage.IsRegNumberContained(regNumber);
    }

    public bool RemoveVehicle(String regNumber)
    {
        return _garage.Retrieve(regNumber);
    }

    public Vehicle FindVehicle(String regNumber)
    {
        return _garage.FindByRegNumber(regNumber);
    }

    public IEnumerable<Vehicle> SearchVehicles(SearchCriteria criteria)
    {
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

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        return _garage;
    }

    public GarageStatistics GetGarageStatistics()
    {
        Console.WriteLine($"_garage.Count(): {_garage.Count()}, _garage.Count: {_garage.Count}");
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
        return _garage.Capacity;
    }

    public int GetCount()
    {
        return _garage.Count;
    }

    // Populate some vehicles to the garage
    public bool InitializeGarage()
    {
        if (_garage == null)
        {
            CreateGarage(20);
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
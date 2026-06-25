using Garage.Interfaces;
using Garage.Factories;
using Garage.Models.Enums;

namespace Garage.Managers;

public class GarageManager
{
    private readonly Dictionary<string, IGarage> _garages;

    public int Count => _garages.Count;

    public string? CurrentGarageName { get; set; }

    public IGarage? CurrentGarage { get => 
            (!string.IsNullOrWhiteSpace(CurrentGarageName)
            && _garages.TryGetValue(CurrentGarageName, out var garage))
        ? garage
        : null;
        }
    public VehicleType CurrentGarageType { get => CurrentGarage?.VehicleType ?? VehicleType.Vehicle; }

    public GarageManager()
    {
        _garages = new Dictionary<string, IGarage>();
    }

    public bool CreateGarage(string name, int capacity, VehicleType vehicleType)
    {
        IGarage garage = GarageFactory.CreateGarage(name, capacity, vehicleType);

        return AddGarage(garage);
    }

    public bool AddGarage(IGarage garage)
    {
        if (IsGarageExisting(garage.Name))
        {
            return false;
        }

        _garages.Add(garage.Name, garage);

        CurrentGarageName = garage.Name;
        /* if (string.IsNullOrWhiteSpace(CurrentGarageName))
        {
            CurrentGarageName = garage.Name;
        } */

        return true;
    }
    public bool UppdateGarage(IGarage garage)
    {
        _garages[garage.Name] = garage;
        return true;
    }

    public bool SelectGarage(string name)
    {
        if (_garages.ContainsKey(name))
        {
            CurrentGarageName = name;
            return true;
        }

        return false;
    }

    public bool IsGarageExisting(string name)
    {
        return _garages.ContainsKey(name);
    }

    public void Clear()
    {
        _garages.Clear();
        CurrentGarageName = null;
    }

    public IEnumerable<string> GetAllGarageNames()
    {
        return _garages.Keys;
    }

    public IEnumerable<IGarage> GetAllGarages()
    {
        return _garages.Values;
    }

}
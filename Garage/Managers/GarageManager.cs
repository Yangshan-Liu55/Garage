using Garage.Interfaces;

namespace Garage.Managers;

public class GarageManager
{
    private readonly Dictionary<string, IGarage> _garages;

    public int Count => _garages.Count;

    public string? CurrentGarageName { get; set; }

    public GarageManager()
    {
        _garages = new Dictionary<string, IGarage>();
    }

    public bool AddGarage(IGarage garage)
    {
        if (IsGarageExisting(garage.Name))
        {
            return false;
        }

        _garages.Add(garage.Name, garage);

        if (string.IsNullOrWhiteSpace(CurrentGarageName))
        {
            CurrentGarageName = garage.Name;
        }

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

    public IGarage? GetCurrentGarage()
    {
        if (!string.IsNullOrWhiteSpace(CurrentGarageName)
            && _garages.TryGetValue(CurrentGarageName, out var garage))
        {
            return garage;
        }
        return null;
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
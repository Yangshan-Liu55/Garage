using System;
using System.Collections;
using System.Collections.Generic;
using Garage.Interfaces;
using Garage.Models.Vehicles;
using Garage.Models.Enums;

namespace Garage.Models;

public class Garage<T> : IVehicleGarage<T> where T : Vehicle
{
    private readonly T?[] _vehicles;
    private readonly string _name = "Garage";
    private readonly int _capacity;
    private readonly Dictionary<string, int> _regNumberIndex;
    private readonly VehicleType _vehicleType;
    private int _count;

    public string Name { get => _name; }
    public int Capacity { get => _capacity; }
    public VehicleType VehicleType { get => _vehicleType; }
    public int Count { get => _count; }

    public double OccupiedSpace => this.Sum(v => v.RequiredSpace);

    public double AvailableSpace => Capacity - OccupiedSpace;

    public Garage(string name, int capacity)
    {
        _name = name;
        _capacity = capacity;
        _vehicleType = VehicleType.Vehicle;

        _vehicles = new T[capacity];
        _count = 0;
        _regNumberIndex = new Dictionary<string, int>();
    }

    public Garage(string name, int capacity, VehicleType vehicleType)
    {
        _name = name;
        _capacity = capacity;
        _vehicleType = vehicleType;

        _vehicles = new T[capacity];
        _count = 0;
        _regNumberIndex = new Dictionary<string, int>();
    }

    public bool Park(T vehicle)
    {
        if (!(Enum.Equals(VehicleType, VehicleType.Vehicle)
                || Enum.Equals(VehicleType, vehicle.VehicleType))
            && vehicle.RequiredSpace <= AvailableSpace) // (_count >= _capacity)
        {
            return false;
        }

        try
        {
            for (int i = 0; i < _capacity; i++)
            {
                if (_vehicles[i] == null)
                {
                    vehicle.RegistrationNumber = vehicle.RegistrationNumber.ToUpper();

                    _regNumberIndex.Add(vehicle.RegistrationNumber, i);
                    _vehicles[i] = vehicle;
                    _count++;

                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool IsRegNumberContained(string regNumber)
    {
        return _regNumberIndex.ContainsKey(regNumber.ToUpper());
    }

    public bool Retrieve(string regNumber)
    {
        try
        {
            regNumber = regNumber.ToUpper();

            if (_regNumberIndex.TryGetValue(regNumber, out int index))
            {
                _vehicles[index] = null;
                _regNumberIndex.Remove(regNumber);
                _count--;

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public T? FindByRegNumber(string regNumber)
    {
        try
        {
            regNumber = regNumber.ToUpper();

            if (_regNumberIndex.TryGetValue(regNumber, out int index))
            {
                return _vehicles[index];
            }

            return null;
        }
        catch
        {
            return null;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T? vehicle in _vehicles)
        {
            if (vehicle is not null)
            {
                yield return vehicle;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
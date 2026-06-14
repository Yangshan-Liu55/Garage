using System;
using System.Collections;
using Garage.Interfaces;
using Garage.Vehicles;

namespace Garage;

public class Garage<T> : IEnumerable<T> where T : Vehicle
{
    private readonly T?[] _vehicles;
    private readonly int _capacity;
    private readonly Dictionary<string, int> _regNumberIndex;
    private int _count;

    public int Capacity { get => _capacity; }
    public int Count { get => _count; }

    public Garage(int capacity)
    {
        _capacity = capacity;
        _vehicles = new T[capacity];
        _count = 0;
        _regNumberIndex = new Dictionary<string, int>();
    }

    public bool Park(T vehicle)
    {
        if (_count >= _capacity)
        {
            return false;
        }

        try
        {
            for (int i = 0; i < _capacity; i++)
            {
                if (_vehicles[i] == null)
                {
                    _vehicles[i] = vehicle;
                    _regNumberIndex.Add(vehicle.RegistrationNumber, i);
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
        return _regNumberIndex.ContainsKey(regNumber);
    }

    public bool Retrieve(string regNumber)
    {
        try
        {
            int index = _regNumberIndex[regNumber];

            _vehicles[index] = null;
            _regNumberIndex.Remove(regNumber);
            _count--;

            return true;
        }
        catch
        {
            return false;
        }
    }

    public Vehicle? FindByRegNumber(string regNumber)
    {
        try
        {
            int index = _regNumberIndex[regNumber];

            return _vehicles[index];
        }
        catch
        {
            return null;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var vehicle in _vehicles)
        {
            if (vehicle != null)
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
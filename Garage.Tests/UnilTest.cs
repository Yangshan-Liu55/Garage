using Xunit;
using System;
using Garage;
using Garage.Interfaces;
using Garage.Vehicles;
using Garage.Enums;

namespace Garage.Tests;

public class UnilTest
{
    [Fact]
    public void CreateGarage_InputCapacityInConstructor()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(100);

        //Act
        int capacity = garage.Capacity;

        //Assert
        Assert.Equal(100, capacity);
    }

    [Fact]
    public void Park_AddACar_ArrayVehiclesContainsTheCar()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(100);
        Car car = new Car("ABC123", "yellow", 4, FuelType.Gasoline);

        //Act
        bool actual = garage.Park(car);

        //Assert
        Assert.True(actual);
    }

}

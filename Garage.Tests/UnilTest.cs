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
        Garage<Vehicle> Garage = new Garage<Vehicle>(100);

        //Act
        int Capacity = Garage.Capacity;

        //Assert
        Assert.Equal(100, Capacity);
    }
}

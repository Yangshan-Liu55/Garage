using Xunit;
using System;
using Garage;
using Garage.Interfaces;
using Garage.Vehicles;
using Garage.Enums;

namespace Garage.Tests;

public class UnilTest
{
    public static IEnumerable<object[]> Vehicles => new List<object[]>
    {
        new object[]
        {
            new Car(
                "CAR123",
                "Red",
                4,
                FuelType.Gasoline)
        },

        new object[]
        {
            new Motorcycle(
                "MC123",
                "Black",
                2,
                600)
        },

        new object[]
        {
            new Bus(
                "BUS123",
                "Blue",
                6,
                50)
        },

        new object[]
        {
            new Boat(
                "BOAT123",
                "White",
                0,
                7.5)
        },

        new object[]
        {
            new Airplane(
                "AIR123",
                "Silver",
                3,
                2)
        }
    };

    [Fact]
    public void GarageConstructor_InputCapacity_ShouldCreateGarageWithCorrectCapacity()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(100);

        //Act
        int capacity = garage.Capacity;

        //Assert
        Assert.Equal(100, capacity);
    }

    [Theory]
    [MemberData(nameof(Vehicles))]
    public void Park_NewVehicle_ShouldReturnTrue(Vehicle vehicle)
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(100);

        // Act
        bool actual = garage.Park(vehicle);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Park_FullGarage_ShouldReturnFalse()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(2);
        garage.Park(
            new Car("ABC123", "yellow", 4, FuelType.Gasoline)
        );
        garage.Park(
            new Car("BCD123", "red", 4, FuelType.Gasoline)
        );

        // Act
        bool actual = garage.Park(
            new Car("CDE123", "blue", 4, FuelType.Gasoline)
        );

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void Park_ExistedRegNumber_ShouldReturnFalse()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(2);
        garage.Park(
            new Car("ABC123", "yellow", 4, FuelType.Gasoline)
        );

        // Act
        bool actual = garage.Park(
            new Car("ABC123", "blue", 4, FuelType.Diesel)
        );

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [MemberData(nameof(Vehicles))]
    public void IsRegNumberContained_ExistedRegNumber_ShouldReturnTrue(Vehicle vehicle)
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(100);
        garage.Park(vehicle);

        //Act
        bool actual = garage.IsRegNumberContained(vehicle.RegistrationNumber);

        //Assert
        Assert.True(actual);
    }

    [Fact]
    public void IsRegNumberContained_ExistedRegNumberMixedCase_ShouldReturnTrue()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(2);
        garage.Park(
            new Car("ABC123", "yellow", 4, FuelType.Gasoline)
        );

        // Act
        bool actual = garage.IsRegNumberContained("aBc123");

        // Assert
        Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Vehicles))]
    public void Retrieve_ParkedVehicle_ShouldReturnTrue(Vehicle vehicle)
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(100);
        garage.Park(vehicle);

        // Act
        bool actual = garage.Retrieve(vehicle.RegistrationNumber);

        // Assert
        Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Vehicles))]
    public void FindByRegNumber_ExistedRegNumber_ShouldReturnVehicle(Vehicle vehicle)
    {
        // Arrange
        Garage<Vehicle> garage = new(100);
        garage.Park(vehicle);

        // Act
        Vehicle? result = garage.FindByRegNumber(vehicle.RegistrationNumber);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Find_ExistedRegNumberMixedCase_ShouldReturnVehicle()
    {
        //Arrange
        Garage<Vehicle> garage = new Garage<Vehicle>(2);
        garage.Park(
            new Car("ABC123", "yellow", 4, FuelType.Gasoline)
        );

        // Act
        Vehicle? result = garage.FindByRegNumber("aBc123");

        // Assert
        Assert.NotNull(result);
    }
    
    [Theory]
    [MemberData(nameof(Vehicles))]
    public void GetEnumerator_ShouldReturnAllVehicles(Vehicle vehicle)
    {
        // Arrange
        Garage<Vehicle> garage = new(100);
        garage.Park(vehicle);

        // Act
        IEnumerable<Vehicle>? result = garage;

        // Assert
        Assert.NotNull(result);
    }

}

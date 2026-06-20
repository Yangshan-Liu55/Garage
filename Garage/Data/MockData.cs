using Garage.Models.Enums;
using Garage.Models.Vehicles;

namespace Garage.Data;

public class MockData
{
    public static IEnumerable<object[]> MockVehicles =>
    [
        [new Car("CAR123", "Red", 4, FuelType.Gasoline)],
        [new Motorcycle("MC123", "Black", 2, 600)],
        [new Bus("BUS123", "Blue", 6, 50)],
        [new Boat("BOAT123", "White", 0, 7.5)],
        [new Airplane("AIR123", "Silver", 3, 2)]
    ];

    public static IEnumerable<object[]> MockCars =>
    [
        [new Car("CAR123", "Red", 4, FuelType.Gasoline)],
        [new Car("CAR124", "white", 4, FuelType.Diesel)],
        [new Car("CAR125", "black", 4, FuelType.Gasoline)],
        [new Car("CAR126", "grey", 4, FuelType.Diesel)],
        [new Car("CAR127", "black", 4, FuelType.Gasoline)]
    ];

    public static IEnumerable<object[]> MockMotorcycles =>
    [
        [new Motorcycle("MC123", "Black", 2, 600)],
        [new Motorcycle("MC124", "white", 2, 400)],
        [new Motorcycle("MC125", "blue", 2, 600)],
        [new Motorcycle("MC126", "white", 2, 500)],
        [new Motorcycle("MC127", "Black", 2, 300)]
    ];

    public static IEnumerable<object[]> MockBuses =>
    [
        [new Bus("BUS123", "Blue", 4, 30)],
        [new Bus("BUS124", "Blue", 4, 30)],
        [new Bus("BUS125", "red", 6, 50)],
        [new Bus("BUS126", "red", 6, 50)],
        [new Bus("BUS127", "red", 6, 50)]
    ];

    public static IEnumerable<object[]> MockBoats =>
    [
        [new Boat("BOAT123", "White", 0, 7.5)],
        [new Boat("BOAT124", "White", 0, 7.5)],
        [new Boat("BOAT125", "White", 0, 7.5)],
        [new Boat("BOAT126", "White", 0, 7.5)],
        [new Boat("BOAT127", "White", 0, 7.5)]
    ];

    public static IEnumerable<object[]> MockAirplanes =>
    [
        [new Airplane("AIR123", "Silver", 3, 2)],
        [new Airplane("AIR124", "Silver", 3, 2)],
        [new Airplane("AIR125", "Silver", 3, 2)],
        [new Airplane("AIR126", "Silver", 3, 2)],
        [new Airplane("AIR127", "Silver", 3, 2)]
    ];

}
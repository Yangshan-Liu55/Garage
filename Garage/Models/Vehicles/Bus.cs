using Garage.Models.Enums;

namespace Garage.Models.Vehicles;

public class Bus : Vehicle
{
    public int NumberOfSeats { get; set; }
    public override VehicleType VehicleType => VehicleType.Bus;

    public Bus(string registrationNumber, string color, int wheels, int numberOfSeats)
        : base(registrationNumber, color, wheels)
    {
        NumberOfSeats = numberOfSeats;
    }

    public override string ToString()
    {
        return base.ToString() + $", number of seats: {NumberOfSeats}";
    }
}
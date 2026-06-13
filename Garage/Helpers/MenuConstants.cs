using Garage.Enums;

namespace Garage.Helpers;
public class MenuConstants
{
    public const string MainTitle = "===== Garage Application =====";
    public const string CreateGarage = "1";
    public const string ManageVehicle = "2";
    public const string ListAllVehicles = "3";
    public const string GarageStatistics = "4";
    public const string InitializeGarage = "5";
    public const string Exit = "0";

    public const string VehicleTitle = "===== Manage Vehicle =====";
    public const string AddVehicle = "1";
    public const string RemoveVehicle = "2";
    public const string FindVehicle = "3";
    public const string SearchVehicles = "4";
    public const string Back = "0";

    public const string AddVehicleTitle = "===== Add Vehicle =====";

    public const string SearchVehiclesTitle = "===== Search Vehicles =====";

    public const string All = "0";

    public static readonly List<MenuItem> MainItems = new List<MenuItem>
    {
        new MenuItem(CreateGarage, "Create Garage"),
        new MenuItem(ManageVehicle, "Manage Vehicle"),
        new MenuItem(ListAllVehicles, "List All Vehicles"),
        new MenuItem(GarageStatistics, "Garage Statistics"),
        new MenuItem(InitializeGarage, "Initialize Garage"),
        new MenuItem(Exit, "Close")
    };

    public static readonly List<MenuItem> VehicleItems = new List<MenuItem>
    {
        new MenuItem(AddVehicle, "Add Vehicle"),
        new MenuItem(RemoveVehicle, "Remove Vehicle"),
        new MenuItem(FindVehicle, "Find Vehicle"),
        new MenuItem(SearchVehicles, "Search Vehicles"),
        new MenuItem(Back, "Back")
    };

    public static readonly List<MenuItem> AddVehicleItems = new List<MenuItem>
    {
        new MenuItem(VehicleType.Car.ToString("d"), "Add " + VehicleType.Car.ToString()),
        new MenuItem(VehicleType.Motorcycle.ToString("d"), "Add " + VehicleType.Motorcycle.ToString()),
        new MenuItem(VehicleType.Bus.ToString("d"), "Add " + VehicleType.Bus.ToString()),
        new MenuItem(VehicleType.Boat.ToString("d"), "Add " + VehicleType.Boat.ToString()),
        new MenuItem(VehicleType.Airplane.ToString("d"), "Add " + VehicleType.Airplane.ToString()),
        new MenuItem(Back, "Back")
    };

    public static readonly List<MenuItem> VehicleTypes = new List<MenuItem>
    {
        new MenuItem(VehicleType.Car.ToString("d"), VehicleType.Car.ToString()),
        new MenuItem(VehicleType.Motorcycle.ToString("d"), VehicleType.Motorcycle.ToString()),
        new MenuItem(VehicleType.Bus.ToString("d"), VehicleType.Bus.ToString()),
        new MenuItem(VehicleType.Boat.ToString("d"), VehicleType.Boat.ToString()),
        new MenuItem(VehicleType.Airplane.ToString("d"), VehicleType.Airplane.ToString()),
        new MenuItem(All, "All")
    };
}
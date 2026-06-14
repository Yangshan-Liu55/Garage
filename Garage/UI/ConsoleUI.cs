using System.Text;
using Garage.Interfaces;
using Garage.Helpers;
using Garage.Handlers;
using Garage.Vehicles;
using Garage.Enums;
using Garage.Statistics;
using Garage.Requests;

namespace Garage.UI;

internal class ConsoleUI : IConsoleUI
{
    private readonly GarageHandler _garageHandler = new GarageHandler();
    public void Start()
    {
        bool exit = false;
        do
        {
            PrintMenu(MenuConstants.MainTitle, MenuConstants.MainItems);

            Console.Write("Choose: ");
            string choice = InputHelpers.ReadLine;
            exit = HandleMainMenu(choice);
        } while (!exit);
    }

    private void PrintMenu(string title, List<MenuItem> items)
    {
        Console.WriteLine();
        Console.WriteLine(title);

        foreach (MenuItem item in items)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    private bool HandleMainMenu(string choice)
    {
        bool result;
        switch (choice)
        {
            case MenuConstants.CreateGarage:
                int capacity = InputHelpers.ReadInt("Capacity of garage: ");
                result = _garageHandler.CreateGarage(capacity);

                Console.WriteLine();
                Console.WriteLine(result ? "Garage is successfully created." : "Failed to create a garage.");
                break;
            case MenuConstants.ManageVehicle:
                ManageVehicle();
                break;
            case MenuConstants.ListAllVehicles:
                ListAllVehicles();
                break;
            case MenuConstants.GarageStatistics:
                PrintGarageStatistics();
                break;
            case MenuConstants.InitializeGarage:
                result = _garageHandler.InitializeGarage();
                Console.WriteLine();
                Console.WriteLine(result
                    ? "Garage is successfully initialized."
                    : "Something went wrong! Garage couldn't be initialized.");
                break;
            case MenuConstants.Exit:
                return true;
            default:
                InvalidChoice();
                HandleMainMenu(InputHelpers.ReadLine);
                break;
        }
        return false;
    }

    private void InvalidChoice()
    {
        Console.WriteLine("Invalid choice. Please choose a valid option number.");
    }

    private void ManageVehicle()
    {
        bool back = false;
        do
        {
            PrintMenu(MenuConstants.VehicleTitle, MenuConstants.VehicleItems);

            Console.Write("Choose: ");
            string choice = InputHelpers.ReadLine;
            back = HandleVehicleMenu(choice);
        } while (!back);
    }

    private bool HandleVehicleMenu(string choice)
    {
        switch (choice)
        {
            case MenuConstants.AddVehicle:
                AddVehicle();
                break;
            case MenuConstants.RemoveVehicle:
                RemoveVehicle();
                break;
            case MenuConstants.FindVehicle:
                FindVehicle();
                break;
            case MenuConstants.SearchVehicles:
                SearchVehicles();
                break;
            case MenuConstants.Back:
                return true;
            default:
                InvalidChoice();
                HandleVehicleMenu(InputHelpers.ReadLine);
                break;
        }
        return false;
    }

    private void AddVehicle()
    {
        bool back = false;
        do
        {
            PrintMenu(MenuConstants.AddVehicleTitle, MenuConstants.AddVehicleItems);

            string choice = InputHelpers.ReadString("Choose: ");
            back = HandleAddVehicleMenu(choice);
        } while (!back);
    }

    private bool HandleAddVehicleMenu(string choice)
    {
        bool success = false;

        if (int.TryParse(choice, out int result)
            && Enum.IsDefined(typeof(VehicleType), result))
        {
            VehicleType vehicleType = (VehicleType)result;
            string regNumber = ReadRegNumber();
            string color = InputHelpers.ReadString("Color: ").ToLower();

            switch (vehicleType)
            {
                case VehicleType.Car:
                    FuelType fuelType = ChooseFuelType();

                    Car car = new Car(regNumber, color, 4, fuelType);

                    success = _garageHandler.AddVehicle(car);

                    break;
                case VehicleType.Motorcycle:
                    int cylinderVolum = InputHelpers.ReadInt("Enter cylinder volumn: ");

                    Motorcycle motorcycle = new Motorcycle(regNumber, color, 2, cylinderVolum);

                    success = _garageHandler.AddVehicle(motorcycle);

                    break;
                case VehicleType.Bus:
                    int numberOfSeats = InputHelpers.ReadInt("Enter number of seats: ");

                    Bus bus = new Bus(regNumber, color, 4, numberOfSeats);

                    success = _garageHandler.AddVehicle(bus);

                    break;
                case VehicleType.Boat:
                    double length = InputHelpers.ReadInt("Enter length: ");

                    Boat boat = new Boat(regNumber, color, 0, length);

                    success = _garageHandler.AddVehicle(boat);

                    break;
                case VehicleType.Airplane:
                    int numberOfEngines = InputHelpers.ReadInt("Enter number of engines: ");

                    Airplane airplane = new Airplane(regNumber, color, 4, numberOfEngines);

                    success = _garageHandler.AddVehicle(airplane);

                    break;
                default:
                    success = false;

                    break;
            }
        }
        else if (choice == MenuConstants.Back)
        {
            return true;
        }
        else
        {
            InvalidChoice();
        }

        Console.WriteLine();
        Console.WriteLine(success
            ? "Vehicle is successfully parked."
            : "Failed to park the vehicle. Please try again.");

        // Return false to continue adding other vehicle. Return true - Choose: 0. Back
        return false;
    }

    private FuelType ChooseFuelType()
    {
        Console.WriteLine("Select fuel type: ");
        Console.WriteLine("1. Gasoline");
        Console.WriteLine("2. Diesel");

        do
        {
            string choice = InputHelpers.ReadLine;
            switch (choice)
            {
                case "1":
                    return FuelType.Gasoline;
                case "2":
                    return FuelType.Diesel;
                default:
                    InvalidChoice();
                    break;
            }
        } while (true);
    }

    private string ReadRegNumber()
    {
        string regNumber;
        do
        {
            regNumber = InputHelpers.ReadStringToUpper("Registration number: ");
            if (!_garageHandler.CheckRegistrationNumber(regNumber))
            {
                return regNumber;
            }
            Console.WriteLine("The number is already registered. Please enter a new one.");
        } while (true);
    }

    private void RemoveVehicle()
    {
        Console.WriteLine();
        Console.WriteLine("===== Remove Vehicle ===== ");
        bool removed = false;
        do
        {
            string regNumber = InputHelpers.ReadStringToUpper("Registration number: ");
            removed = _garageHandler.RemoveVehicle(regNumber);
            if (removed)
            {
                Console.WriteLine();
                Console.WriteLine("Vehicle is successfully retrieved.");
            }
            else
            {
                Console.WriteLine("The number is not registered. Please enter a valid one.");
            }
        } while (!removed);
    }

    private void FindVehicle()
    {
        Console.WriteLine();
        Console.WriteLine("===== Find Vehicle ===== ");
        bool result = false;
        Vehicle? vehicle;
        do
        {
            string regNumber = InputHelpers.ReadStringToUpper("Registration number: ");
            vehicle = _garageHandler.FindVehicle(regNumber);
            if (vehicle != null)
            {
                Console.WriteLine("Vehicle is found:");
                Console.WriteLine(vehicle.ToString());
                result = true;
            }
            else
            {
                Console.WriteLine("Vehicle is not found! Please enter a valid registration number.");
                Console.WriteLine();
            }
        } while (!result);
    }

    private void SearchVehicles()
    {
        Console.WriteLine();
        Console.WriteLine($"===== {MenuConstants.SearchVehiclesTitle} ===== ");
        Console.WriteLine("*** Supports multiple selections separated by commas *** ");
        Console.WriteLine("*** For example, Select Car and Motorcycle: 1,2 *** ");
        Console.WriteLine("*** Leave empty to ignore selection (i.e. Select all options) *** ");

        SearchCriteria criteria = new SearchCriteria();
        criteria = ChooseVehicleTypes(criteria);
        criteria = ChooseColors(criteria);
        criteria = ChooseWheels(criteria);

        IEnumerable<Vehicle>? vehicles = _garageHandler.SearchVehicles(criteria);

        if (vehicles == null)
        {
            Console.WriteLine();
            Console.WriteLine("No garage is defined or no vehicle is parked. Please initialize garage.");
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine($"===== {vehicles.Count()} vehicle(s) found =====");

        foreach (Vehicle vehicle in vehicles)
        {
            sb.AppendLine(vehicle.ToString());
        }

        Console.WriteLine(sb.ToString());
    }

    private SearchCriteria ChooseVehicleTypes(SearchCriteria criteria)
    {
        PrintMenu("Vehicle Type options: ", MenuConstants.VehicleTypes);
        Console.Write("Select Vehicle Type(s): ");
        string input = InputHelpers.ReadLine;
        string[] array = input.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string str in array)
        {
            if (Enum.TryParse(str.Trim(), out VehicleType type))
            {
                if (Enum.IsDefined(typeof(VehicleType), type))
                {
                    criteria.VehicleTypes.Add(type);
                }

            }
        }

        return criteria;
    }

    private SearchCriteria ChooseColors(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.Write("Select color(s):");
        string input = InputHelpers.ReadLine.ToLower();

        criteria.Colors = input.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(color => color.Trim()).ToArray();

        return criteria;
    }

    private SearchCriteria ChooseWheels(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.Write("Select wheel(s):");
        string input = InputHelpers.ReadLine.ToLower();

        string[] array = input.Replace(' ', ',')
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        int i = -1;
        criteria.Wheels = Array.ConvertAll(
            array,
            s => int.TryParse(s, out i) ? i : -1
        );

        return criteria;
    }

    private void ListAllVehicles()
    {
        IEnumerable<Vehicle>? vehicles = _garageHandler.GetAllVehicles();
        if (vehicles == null)
        {
            Console.WriteLine("No garage is defined or no vehicle is parked. Please initialize garage.");
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("");
            sb.AppendLine("===== List All Vehicles =====");

            foreach (var vehicle in vehicles)
            {
                sb.AppendLine(vehicle.ToString());
            }

            sb.AppendLine("");
            sb.AppendLine($"Total parked vehicles: {vehicles.Count()}");

            Console.WriteLine(sb.ToString());
        }
    }

    private void PrintGarageStatistics()
    {
        GarageStatistics? stats = _garageHandler.GetGarageStatistics();
        if (stats == null)
        {
            Console.WriteLine("No garage is defined or no vehicle is parked. Please initialize garage.");
            return;
        }

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("===== Garage Statistics =====");
        sb.AppendLine("*** List vehicle types and how many of each are in the garage ***");
        sb.AppendLine("");
        sb.AppendLine("Parked vehicles: ");

        foreach (var group in stats.CountsByVehicleTypes)
        {
            sb.AppendLine($"{group.Key}: {group.Value}");
        }
        sb.AppendLine("");
        sb.AppendLine($"Total parked vehicles: {stats.TotalParkedVehicles}");
        sb.AppendLine($"Total available parking spots: {stats.Capacity - stats.TotalParkedVehicles}");
        sb.AppendLine($"Garage capacity: {stats.Capacity}");

        Console.WriteLine(sb.ToString());
    }
}
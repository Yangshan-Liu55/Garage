using System.Text;
using Garage.Interfaces;
using Garage.Helpers;
using Garage.Handlers;
using Garage.Models;
using Garage.Models.Vehicles;
using Garage.Models.Enums;
using Garage.Models.Statistics;
using Garage.Managers;

namespace Garage.UI;

internal class ConsoleUI : IConsoleUI
{
    private readonly GarageManager _garageManager;
    private readonly GarageHandler _garageHandler;

    public ConsoleUI()
    {
        _garageManager = new GarageManager();
        _garageHandler = new GarageHandler(_garageManager);
    }

    public void Start()
    {
        bool exit = false;
        do
        {
            PrintMenu(MenuConstants.MainTitle, MenuConstants.MainItems, true);

            exit = HandleMainMenu();
        } while (!exit);
    }

    private void PrintMenu(string title, List<MenuItem> items, bool printCurrentGarageName)
    {
        Console.WriteLine();
        Console.WriteLine(title);

        if (printCurrentGarageName)
        {
            PrintCurrentGarageName();
        }

        foreach (MenuItem item in items)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    private void PrintCurrentGarageName()
    {
        string name = "No existing garage. Please create garage!";
        if (!string.IsNullOrWhiteSpace(_garageManager.CurrentGarageName))
        {
            name = _garageManager.CurrentGarageName;
        }

        Console.WriteLine($"***** Current Garage: {name} *****");
        Console.WriteLine();
    }

    private bool HandleMainMenu()
    {
        Console.Write("Choose: ");
        string choice = InputHelpers.ReadLine;

        switch (choice)
        {
            case MenuConstants.CreateGarage:
                string garageName = InputHelpers.ReadString("Garage name: ");

                if (_garageManager.IsGarageExisting(garageName))
                {
                    Console.WriteLine("Garage is already existing!");
                }
                else
                {
                    int capacity = InputHelpers.ReadInt("Capacity of garage: ");

                    IGarage garage = new Garage<Vehicle>(garageName, capacity);
                    // IGarage garage = _garageHandler.CreateGarage(garageName, capacity);
                    bool success = _garageManager.AddGarage(garage);

                    Console.WriteLine();
                    Console.WriteLine(success
                        ? "Garage is successfully created."
                        : "Failed to create a garage. Garage is already existing!");
                }
                break;
            case MenuConstants.SelectGarage:
                SelectGarage();
                break;
            case MenuConstants.ManageVehicle:
                ManageVehicle();
                break;
            case MenuConstants.Exit:
                return true;
            default:
                InvalidChoice();
                HandleMainMenu();
                break;
        }
        return false;
    }

    private void InvalidChoice()
    {
        Console.WriteLine("Invalid choice. Please choose a valid option number.");
    }

    private void SelectGarage()
    {
        List<MenuItem> garageItems = new List<MenuItem>();

        if (_garageManager.Count < 1)
        {
            PrintMenu(MenuConstants.SelectGarageTitle, garageItems, true);

            Console.WriteLine("Please create a garage before you can select it.");
            return;
        }

        string[] garageNames = _garageManager.GetAllGarageNames().ToArray();

        for (int i = 0; i < garageNames.Length; i++)
        {
            MenuItem menuItem = new MenuItem((i + 1).ToString(), garageNames[i]);
            garageItems.Add(menuItem);
        }
        garageItems.Add(new MenuItem("0", "Back"));

        PrintMenu(MenuConstants.SelectGarageTitle, garageItems, true);

        bool back = false;
        do
        {
            Console.WriteLine();
            Console.Write("Choose: ");
            string choice = InputHelpers.ReadLine;

            if (int.TryParse(choice, out int index) && index < (garageNames.Length + 1))
            {
                if (index != 0)
                {
                    bool success = _garageManager.SelectGarage(garageNames[index - 1]);

                    Console.WriteLine(success
                        ? "The current garage is successfully changed!"
                        : "Failed to change the current garage. Selected garage is not existing!");
                }

                back = true;
            }
            else
            {
                InvalidChoice();
            }
        } while (!back);
    }

    private void ManageVehicle()
    {
        bool back = false;
        do
        {
            PrintMenu(MenuConstants.VehicleTitle, MenuConstants.VehicleItems, true);

            if (string.IsNullOrWhiteSpace(_garageManager.CurrentGarageName))
            {
                back = true;
            }
            else
            {

                back = HandleVehicleMenu();
            }
        } while (!back);
    }

    private bool HandleVehicleMenu()
    {
        Console.Write("Choose: ");
        string choice = InputHelpers.ReadLine;

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
            case MenuConstants.ListAllVehicles:
                ListAllVehicles();
                break;
            case MenuConstants.GarageStatistics:
                PrintGarageStatistics();
                break;
            case MenuConstants.InitializeGarage:
                InitializeCurrentGarage();
                break;
            case MenuConstants.Back:
                return true;
            default:
                InvalidChoice();
                HandleVehicleMenu();
                break;
        }
        return false;
    }

    private void AddVehicle()
    {
        bool back = false;
        do
        {
            PrintMenu(MenuConstants.AddVehicleTitle, MenuConstants.AddVehicleItems, true);

            back = HandleAddVehicleMenu();
        } while (!back);
    }

    private bool HandleAddVehicleMenu()
    {
        string choice = InputHelpers.ReadString("Choose: ");
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
                    int cylinderVolume = InputHelpers.ReadInt("Enter cylinder volumn: ");

                    Motorcycle motorcycle = new Motorcycle(regNumber, color, 2, cylinderVolume);

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
        PrintCurrentGarageName();

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
        criteria = ChooseFuelTypes(criteria);
        criteria = ChooseCylinderVolumes(criteria);
        criteria = ChooseNumbersOfSeats(criteria);
        criteria = ChooseLengths(criteria);
        criteria = ChooseNumbersOfEngines(criteria);

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
        sb.AppendLine($"***** Current Garage: {_garageManager.CurrentGarageName} *****");

        foreach (Vehicle vehicle in vehicles)
        {
            sb.AppendLine(vehicle.ToString());
        }

        Console.WriteLine(sb.ToString());
    }

    private SearchCriteria ChooseVehicleTypes(SearchCriteria criteria)
    {
        PrintMenu("Vehicle Type options: ", MenuConstants.VehicleTypes, false);

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

    private SearchCriteria ChooseFuelTypes(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.WriteLine("Fuel type options: ");
        Console.WriteLine($"{FuelType.Gasoline.ToString("d")}. Gasoline");
        Console.WriteLine($"{FuelType.Diesel.ToString("d")}. Diesel");

        Console.Write("Select fuel type(s): ");
        string input = InputHelpers.ReadLine;
        string[] array = input.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string str in array)
        {
            if (Enum.TryParse(str.Trim(), out FuelType type))
            {
                if (Enum.IsDefined(typeof(FuelType), type))
                {
                    criteria.FuelTypes.Add(type);
                }

            }
        }

        return criteria;
    }

    private SearchCriteria ChooseCylinderVolumes(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.Write("Select cylinder volume(s):");
        string input = InputHelpers.ReadLine.ToLower();

        string[] array = input.Replace(' ', ',')
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        criteria.CylinderVolumes = Array.ConvertAll(
            array,
            s => int.TryParse(s, out int i) ? i : -1
        );

        return criteria;
    }

    private SearchCriteria ChooseNumbersOfSeats(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.Write("Select number(s) of seats:");
        string input = InputHelpers.ReadLine.ToLower();

        string[] array = input.Replace(' ', ',')
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        criteria.NumbersOfSeats = Array.ConvertAll(
            array,
            s => int.TryParse(s, out int i) ? i : -1
        );

        return criteria;
    }

    private SearchCriteria ChooseLengths(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.Write("Select length(s):");
        string input = InputHelpers.ReadLine.ToLower();

        string[] array = input.Replace(' ', ',')
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        criteria.Lengths = Array.ConvertAll(
            array,
            s => double.TryParse(s, out double i) ? i : -1
        );

        return criteria;
    }

    private SearchCriteria ChooseNumbersOfEngines(SearchCriteria criteria)
    {
        Console.WriteLine();
        Console.Write("Select number(s) of engines:");
        string input = InputHelpers.ReadLine.ToLower();

        string[] array = input.Replace(' ', ',')
            .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        criteria.NumbersOfSeats = Array.ConvertAll(
            array,
            s => int.TryParse(s, out int i) ? i : -1
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
            sb.AppendLine($"***** Current Garage: {_garageManager.CurrentGarageName} *****");

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
        sb.AppendLine($"***** Current Garage: {_garageManager.CurrentGarageName} *****");
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

    private void InitializeCurrentGarage()
    {
        bool success = _garageHandler.InitializeGarage();

        Console.WriteLine();
        Console.WriteLine(success
            ? "The current garage is successfully initialized."
            : "Failed to initialize the current garage. The current garage is not existing!");
    }
}
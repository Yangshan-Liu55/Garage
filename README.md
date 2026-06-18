# Garage Application

A C# console application for learning Comprehensive C# Concepts, including **Inheritance**, and **Polymorphism**.

## Purpose

This garage application simulates a simple garage.

It provides features: manage several garages and vehicles in them, park vehicles, retrieve vehicles, search vehicles, and list all information including garages status and characteristics of vehicles.

All these in a console application with a main menu and submenus.

## Class Structure

### GarageHandler, GarageManager

* Using `GarageHandler` to Centralized data processing between `ConsoleUI` and `Garage`.  

Program.cs handles execution, ConsoleUI handles user interactions,  
and GarageManager processes garages data, GarageHandler processes vehicles data.

### Enum
* `VehicleType` Car, Motorcycle, Bus, Boat, Airplane.

### Interface

* `IVehicle` RegistrationNumber, Color, Wheels.

### Base Class

* `Vehicle` abstract VehicleType: in subclass overrides its own VehicleType.

abstract double RequiredSpace: e.g. Car occupies 1 spot, Motorcycle occupies 1/3 spots, Bus occupies 1.5 spots, Boat occupies 2 spots, Airplane occupies 3 spots.

### Derived Classes

Examples include:

* `Car`
* `Motorcycle`
* `Bus`
* `Boat`
* `Airplane`

## Main Features

### Create Garage

* Add a garage to Garage Manager.

### Manage Vehicle

* Add/Remove/Find a vehicle to/from the selected current garage.
* Search/List vehicles from the selected current garage.

### Load/Save Garages Data

* Load/Save data from/to file path. `/Data/garages.json`.`LoadSystemData()`, `SaveSystemData()`

### Auto Load/Save Garages Data

* When application starts/exits, it automatically Loads/Saves data from/to `/Data/autosave.json`.

### Configuration

* Set Default Capacity for garage using `Microsoft.Extensions.Configuration`.
* Define Default Capacity in `appsettings.json`.
* Everywhere can get value: `int capacity = ConfigurationHelper.DefaultCapacity;`.

### XUnit test
We limit ourselves to testing the public methods in the Garage class.

## Command

## Run project

```text
cd Garage
dotnet run
```

## Run unit test project

```text
cd Garage.Tests
dotnet test
```

## Example Console Output

### Start

```text

System data is automatically loaded from Data/autosave.json.

================ Garage Application ================
***** Current Garage: Garage 1 *****
***** Available spot(s): 4.35 *****

1. Create Garage
2. Select Garage
3. Manage Vehicles
4. Save System Data
5. Load System Data
0. Close

Choose: 1

========== Create A Garage ==========
***** Current Garage: Garage 1 *****
Garage name: T-central Garage
Set Default Capacity(45) for Garage?(Y/N): 
y

Garage is successfully created.
Now you can go to Select Garage menu to select it.

================ Garage Application ================
***** Current Garage: Garage 1 *****
***** Available spot(s): 4.35 *****

1. Create Garage
2. Select Garage
3. Manage Vehicles
4. Save System Data
5. Load System Data
0. Close

Choose: 2

========== Select A Garage ==========
***** Current Garage: Garage 1 *****
***** Available spot(s): 4.35 *****

1. General Garage
2. Hangar
3. Motorcykel Garage
4. Garage 1
5. T-central Garage
0. Back


Selct: 5
The current garage is successfully changed!

================ Garage Application ================
***** Current Garage: T-central Garage *****
***** Available spot(s): 45 *****

1. Create Garage
2. Select Garage
3. Manage Vehicles
4. Save System Data
5. Load System Data
0. Close

Choose: 
```

### Manage Vehicles

```text
Choose: 3

========== Manage Vehicles ==========
***** Current Garage: T-central Garage *****
***** Available spot(s): 45 *****

1. Add Vehicle
2. Remove Vehicle
3. Find Vehicle
4. Search Vehicles
5. List All Vehicles
6. Garage Statistics
7. Initialize Current Garage
0. Back

Choose: 7

The current garage is successfully initialized.

========== Manage Vehicles ==========
***** Current Garage: T-central Garage *****
***** Available spot(s): 5.85 *****

1. Add Vehicle
2. Remove Vehicle
3. Find Vehicle
4. Search Vehicles
5. List All Vehicles
6. Garage Statistics
7. Initialize Current Garage
0. Back

Choose: 6

===== Garage Statistics =====
*** List vehicle types and how many of each are in the garage ***
***** Current Garage: T-central Garage *****

Parked vehicles: 
Car: 5
Motorcycle: 5
Bus: 5
Boat: 5
Airplane: 5

Total parked vehicles: 25
Total occupied parking spots: 39.15
Available parking spots: 5.85
Garage capacity: 45

```

### Exit

```text

================ Garage Application ================
***** Current Garage: T-central Garage *****
***** Available spot(s): 5.85 *****

1. Create Garage
2. Select Garage
3. Manage Vehicles
4. Save System Data
5. Load System Data
0. Close

Choose: 0

System data is automatically saved to Data/autosave.json!

```
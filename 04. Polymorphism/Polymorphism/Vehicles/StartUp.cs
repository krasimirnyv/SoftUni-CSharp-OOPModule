using Vehicles.Enums;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles;
public class StartUp
{
    public static void Main(string[] args)
    {
        IVehicle car = CreateVehicle(Console.ReadLine());
        IVehicle truck = CreateVehicle(Console.ReadLine());
        IVehicle bus = CreateVehicle(Console.ReadLine());
        
        // Set the bus people state to default "WithPeople" for the initial state
        (bus as Bus).BusPeople = BusPeople.WithPeople;

        Dictionary<string, IVehicle> vehicles = new Dictionary<string, IVehicle>
        {
            { "Car", car },
            { "Truck", truck },
            { "Bus", bus }
        };
        
        ExecuteCommands(int.Parse(Console.ReadLine()), vehicles);
        
        PrintVehicleWithRemainingFuel(vehicles);
    }

    private static IVehicle CreateVehicle(string input)
    {
        string[] vehicleInfo = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string vehicleType = vehicleInfo[0];
        double fuelQuantity = double.Parse(vehicleInfo[1]);
        double fuelConsumption = double.Parse(vehicleInfo[2]);
        double tankCapacity = double.Parse(vehicleInfo[3]);
        
        return vehicleType switch
        {
            "Car" => new Car(fuelQuantity, fuelConsumption, tankCapacity),
            "Truck" => new Truck(fuelQuantity, fuelConsumption, tankCapacity),
            "Bus" => new Bus(fuelQuantity, fuelConsumption, tankCapacity),
            _ => throw new InvalidOperationException("Unknown vehicle type")
        };
    }

    private static void ExecuteCommands(int numberOfCommands, Dictionary<string, IVehicle> vehicles)
    {
        for (int i = 0; i < numberOfCommands; i++)
        {
            string[] commandArgs = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = commandArgs[0];
            string vehicleType = commandArgs[1];
            double value = double.Parse(commandArgs[2]);
            
            IVehicle vehicle = vehicles[vehicleType];
            
            try
            {
                switch (command)
                {
                    case "Drive":
                        string driveMessage = vehicle.Drive(value);
                        Console.WriteLine(driveMessage);
                        break;
                    case "DriveEmpty": // works only for Bus
                        (vehicle as Bus).BusPeople = BusPeople.WithoutPeople;
                        driveMessage = vehicle.Drive(value);
                        Console.WriteLine(driveMessage);
                    
                        (vehicle as Bus).BusPeople = BusPeople.WithPeople; // Reset to default state
                        break;
                    case "Refuel":
                        vehicle.Refuel(value); break;
                }
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
    
    private static void PrintVehicleWithRemainingFuel(Dictionary<string, IVehicle> vehicles)
    {
        foreach (var kvpVehicle in vehicles)
            Console.WriteLine(kvpVehicle.Value);
    }
}

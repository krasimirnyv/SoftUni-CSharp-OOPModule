using Vehicles.Models.Interfaces;

namespace Vehicles.Models;
public abstract class Vehicle : IVehicle
{
    private double _fuelQuantity;
    private double _fuelConsumption;
    private double _tankCapacity;

    protected virtual double AirConditionerIncreasedConsumption => 0.0;
    protected virtual double RefuelLosses => 1.0;
    
    protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
    {
        TankCapacity = tankCapacity;
        FuelQuantity = fuelQuantity;
        FuelConsumption = fuelConsumption;
    }

    public double FuelQuantity
    {
        get => _fuelQuantity;
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException("Fuel quantity cannot be negative");
            }

            if (value > TankCapacity)
            {
                _fuelQuantity = 0.0;
            }
            else
            {
                _fuelQuantity = value;
            }
        }
    }

    public double FuelConsumption 
    { 
        get => _fuelConsumption;
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException("Fuel consumption cannot be negative");
            }

            _fuelConsumption = value;
        } 
    }

    public double TankCapacity
    {
        get => _tankCapacity;
        protected set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Tank capacity cannot be null or negative");
            }

            _tankCapacity = value;
        }
    }
    
    public string Drive(double distance)
    {
        double fuelNeeded = distance * (FuelConsumption + AirConditionerIncreasedConsumption);
        if (fuelNeeded > FuelQuantity)
        {
            throw new ArgumentException($"{GetType().Name} needs refueling");
        }
        
        FuelQuantity -= fuelNeeded;
        return $"{GetType().Name} travelled {distance} km";
    }

    public void Refuel(double liters)
    {
        if (liters <= 0)
        {
            throw new ArgumentException("Fuel must be a positive number");
        }

        if (FuelQuantity + (liters * RefuelLosses) > TankCapacity)
        {
            throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
        }

        FuelQuantity += liters * RefuelLosses;
    }

    public override string ToString()
        => $"{GetType().Name}: {FuelQuantity:F2}";
}
namespace Vehicles.Models;

public class Truck : Vehicle
{
    
    protected override double AirConditionerIncreasedConsumption => 1.6;
    protected override double RefuelLosses => 0.95;
    
    public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
    }
}
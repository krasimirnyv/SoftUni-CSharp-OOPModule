using Vehicles.Enums;

namespace Vehicles.Models;

public class Bus : Vehicle
{
    protected override double AirConditionerIncreasedConsumption
        => BusPeople == BusPeople.WithPeople ? 1.4 : 0.0;
    
    public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
    }
    
    public BusPeople BusPeople { get; set; }
}
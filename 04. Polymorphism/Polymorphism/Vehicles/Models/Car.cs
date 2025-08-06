namespace Vehicles.Models;

public class Car : Vehicle
{
    protected override double AirConditionerIncreasedConsumption => 0.9;

    public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
        : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
    }
}
namespace NeedForSpeed;

public abstract class Vehicle
{
    private const double DefaultFuelConsumption = 1.25;
    
    protected Vehicle(int horsePower, double fuel)
    {
        HorsePower = horsePower;
        Fuel = fuel;
    }

    public virtual double FuelConsumption
        => DefaultFuelConsumption;
    public double Fuel { get; set; }
    public int HorsePower { get; set; }

    public virtual void Drive(double kilometers)
    { 
        Fuel -= kilometers * FuelConsumption;
    }
}
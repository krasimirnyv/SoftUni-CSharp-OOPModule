using Vehicles.Enums;

namespace Vehicles.Models.Interfaces;

public interface IVehicle
{
    double FuelQuantity { get; }
    
    double FuelConsumption { get; }
    
    double TankCapacity { get; }

    string Drive(double distance);
    
    void Refuel(double liters);
}
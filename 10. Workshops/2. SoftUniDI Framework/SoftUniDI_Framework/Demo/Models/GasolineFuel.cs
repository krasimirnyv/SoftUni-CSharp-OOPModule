using Demo.Interfaces;

namespace Demo.Models;

public class GasolineFuel : IFuel
{
    public GasolineFuel() 
    {
        Type = "Gasoline";
    }

    public string Type { get; }
}
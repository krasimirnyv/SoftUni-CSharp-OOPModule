using Demo.Interfaces;

namespace Demo.Models;

public class DieselFuel : IFuel
{ 
    public DieselFuel() 
    {
        Type = "Diesel";
    }

    public string Type { get; private set; }
}
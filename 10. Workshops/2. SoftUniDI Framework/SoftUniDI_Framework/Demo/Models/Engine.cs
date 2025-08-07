using SoftUniDI_Framework.Attributes;

namespace Demo.Models;

using Interfaces;

public class Engine : IEngine
{
    [Inject]
    public Engine([Named(nameof(DieselFuel))] IFuel fuel)
    {
        Type = nameof(DieselFuel);
        Fuel = fuel;
    }

    public string Type { get; private set; }

    public IFuel Fuel { get; private set; }
}
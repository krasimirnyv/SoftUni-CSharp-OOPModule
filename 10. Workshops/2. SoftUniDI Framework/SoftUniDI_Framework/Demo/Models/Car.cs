using Demo.Interfaces;
using SoftUniDI_Framework.Attributes;

namespace Demo.Models;

public class Car : ICar
{
    [Inject]
    public Car(IEngine engine, ITyres tyres, ISuspension suspension)
    {
        Engine = engine;
        Tyres = tyres;
        Suspension = suspension;
    }
    
    public IEngine Engine { get; private set; }
    public ITyres Tyres { get; private set; }
    public ISuspension Suspension { get; private set; }

    public void Drive()
    {
        Console.WriteLine($"Driving with {Engine.Type} {Engine.GetType().Name.ToLower()}, " +
                          $"{Tyres.Type} {Tyres.GetType().Name.ToLower()}, " +
                          $"and {Suspension.Type} {Suspension.GetType().Name.ToLower()}.");
    }
}
namespace Demo.Interfaces;

public interface IEngine
{
    string Type { get; }

    IFuel Fuel { get; }
}
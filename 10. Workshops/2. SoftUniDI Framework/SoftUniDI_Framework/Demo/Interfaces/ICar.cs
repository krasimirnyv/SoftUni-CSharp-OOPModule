namespace Demo.Interfaces;

public interface ICar
{
    IEngine Engine { get; }
    ITyres Tyres { get; }
    ISuspension Suspension { get; }

    void Drive();
}
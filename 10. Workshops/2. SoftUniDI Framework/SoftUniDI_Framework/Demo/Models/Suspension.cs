using Demo.Interfaces;

namespace Demo.Models;

public class Suspension : ISuspension
{
    public Suspension()
    {
        Type = "SportSuspension";
    }

    public string Type { get; private set; }
}
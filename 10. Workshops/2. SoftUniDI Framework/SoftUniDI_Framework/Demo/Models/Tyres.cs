using Demo.Interfaces;

namespace Demo.Models;

public class Tyres : ITyres
{
    public Tyres()
    {
        Type = "Continental";
    }

    public string Type { get; private set; }
}
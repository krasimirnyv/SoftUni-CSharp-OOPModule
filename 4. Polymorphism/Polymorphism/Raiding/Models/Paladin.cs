namespace Raiding.Models;

public class Paladin : Hero
{
    private const int PaladinPower = 100;
    
    public Paladin(string name) 
        : base(name, PaladinPower)
    {
    }

    public override string CastAbility() 
        => $"{GetType().Name} - {Name} healed for {Power}";
}
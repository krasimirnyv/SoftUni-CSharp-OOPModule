namespace Raiding.Models;

public class Warrior : Hero
{
    private const int WarriorPower = 100;
    
    public Warrior(string name) 
        : base(name, WarriorPower)
    {
    }
    
    public override string CastAbility()
        => $"{GetType().Name} - {Name} hit for {Power} damage";
}
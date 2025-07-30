using Raiding.Factory.Interfaces;
using Raiding.Models;
using Raiding.Models.Interfaces;

namespace Raiding.Factory;

public class HeroFactory : IHeroFactory
{
    public IHero CreateHero(string type, string name)
    {
        return type switch
        {
            "Druid" => new Druid(name),
            "Paladin" => new Paladin(name),
            "Rogue" => new Rogue(name),
            "Warrior" => new Warrior(name),
            _ => throw new ArgumentException("Invalid hero!")
        };
    }
}
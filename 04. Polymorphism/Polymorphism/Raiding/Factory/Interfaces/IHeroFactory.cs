using Raiding.Models.Interfaces;

namespace Raiding.Factory.Interfaces;

public interface IHeroFactory
{
    IHero CreateHero(string type, string name);
}
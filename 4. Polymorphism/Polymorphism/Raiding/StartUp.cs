using Raiding.Factory;
using Raiding.Factory.Interfaces;
using Raiding.Models.Interfaces;

namespace Raiding;

public class StartUp
{
    public static void Main(string[] args)
    {
        IHeroFactory factory = new HeroFactory();
        List<IHero> heroes = new List<IHero>();
        
        int count = int.Parse(Console.ReadLine());
        while (count > 0)
        {
            string name = Console.ReadLine();
            string type = Console.ReadLine();

            try
            {
                IHero hero = factory.CreateHero(type, name);
                heroes.Add(hero);
                count--;
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            } 
        }

        foreach (IHero hero in heroes)
            Console.WriteLine(hero.CastAbility());

        int totalHeroPower = heroes.Sum(h => h.Power);
        int bossPower = int.Parse(Console.ReadLine());

        Console.WriteLine(totalHeroPower >= bossPower ? "Victory!" : "Defeat...");
    }
}
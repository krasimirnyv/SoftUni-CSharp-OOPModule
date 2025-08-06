using WildFarm.Factories.Interfaces;
using WildFarm.Models.Food;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factories;

public class FoodFactory : IFoodFactory
{
    public IFood CreateFood(string type, int quantity)
    {
        return type switch
        {
            "Vegetable" => new Vegetable(quantity),
            "Fruit" => new Fruit(quantity),
            "Meat" => new Meat(quantity),
            "Seeds" => new Seeds(quantity),
            _ => throw new ArgumentException("Invalid food type!")
        };
    }
}
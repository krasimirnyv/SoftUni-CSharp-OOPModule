using WildFarm.Models.Animals.Abstract;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animals;

public class Mouse : Mammal
{
    private const double MouseWeightMultiplier = 0.10;
    private const string MouseSound = "Squeak";
    
    public Mouse(string name, double weight, string livingRegion) 
        : base(name, weight, livingRegion)
    {
    }

    protected override double WeightMultiplier => MouseWeightMultiplier;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
        => new HashSet<Type> { typeof(Vegetable), typeof(Fruit) };

    public override string ProduceSound()
        => MouseSound;

    public override string ToString()
        => base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
}
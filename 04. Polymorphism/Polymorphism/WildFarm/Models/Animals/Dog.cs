using WildFarm.Models.Animals.Abstract;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animals;

public class Dog : Mammal
{
    private const double DogWeightMultiplier = 0.40;
    private const string DogSound = "Woof!";
    
    public Dog(string name, double weight, string livingRegion) 
        : base(name, weight, livingRegion)
    {
    }

    protected override double WeightMultiplier => DogWeightMultiplier;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
        => new HashSet<Type> { typeof(Meat) };

    public override string ProduceSound()
        => DogSound;
    
    public override string ToString()
        => base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
}
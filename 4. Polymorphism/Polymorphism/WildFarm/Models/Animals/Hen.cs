using WildFarm.Models.Animals.Abstract;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animals;

public class Hen : Bird
{
    private const double HenWeightMultiplier = 0.35;
    private const string HenSound = "Cluck";
    
    public Hen(string name, double weight, double wingSize) 
        : base(name, weight, wingSize)
    {
    }

    protected override double WeightMultiplier => HenWeightMultiplier;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
        => new HashSet<Type> { typeof(Vegetable), typeof(Fruit), typeof(Meat), typeof(Seeds) };

    public override string ProduceSound()
        => HenSound;
}
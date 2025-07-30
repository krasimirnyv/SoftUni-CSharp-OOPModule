using WildFarm.Models.Animals.Abstract;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animals;

public class Owl : Bird
{
    private const double OwlWeightMultiplier = 0.25;
    private const string OwlSound = "Hoot Hoot";
    
    public Owl(string name, double weight, double wingSize) 
        : base(name, weight, wingSize)
    {
    }

    protected override double WeightMultiplier => OwlWeightMultiplier;
    
    protected override IReadOnlyCollection<Type> PreferredFoodTypes 
        => new HashSet<Type> { typeof(Meat) };
    
    public override string ProduceSound() 
        => OwlSound;
}
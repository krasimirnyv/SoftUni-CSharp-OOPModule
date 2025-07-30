using WildFarm.Models.Animals.Abstract;
using WildFarm.Models.Food;

namespace WildFarm.Models.Animals;

public class Tiger : Feline
{
    private const double TigerWeightMultiplier = 1.00;
    private const string TigerSound = "ROAR!!!";
    
    public Tiger(string name, double weight, string livingRegion, string breed) 
        : base(name, weight, livingRegion, breed)
    {
    }

    protected override double WeightMultiplier => TigerWeightMultiplier;

    protected override IReadOnlyCollection<Type> PreferredFoodTypes
        => new HashSet<Type> { typeof(Meat) };

    public override string ProduceSound()
        => TigerSound;
}
namespace PizzaCalories;

public class Topping
{
    private const double CaloriesPerGram = 2;
    private const double MinWeight = 1;
    private const double MaxWeight = 50;
    
    private string toppingType;
    private double weight;

    private readonly IReadOnlyDictionary<string, double> caloriesPerToppingType;
    
    public Topping(string toppingType, double weight)
    {
        caloriesPerToppingType = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            { "meat", 1.2 },
            { "veggies", 0.8 },
            { "cheese", 1.1 },
            { "sauce", 0.9 }
        };
        
        ToppingType = toppingType;
        Weight = weight;
    }

    public string ToppingType
    {
        get => toppingType;
        private set
        {
            if (!caloriesPerToppingType.ContainsKey(value))
            {
                throw new ArgumentException($"Cannot place {value} on top of your pizza.");
            }
            
            toppingType = value;
        }
    }
    
    public double Weight
    {
        get => weight;
        private set
        {
            if (value is < MinWeight or > MaxWeight)
            {
                throw new ArgumentException($"{ToppingType} weight should be in the range [{MinWeight}..{MaxWeight}].");
            }
            
            weight = value;
        }
    }

    public double GetToppingCalories
    {
        get
        {
            double toppingTypeModifier = caloriesPerToppingType[ToppingType];
            
            return (CaloriesPerGram * Weight) * toppingTypeModifier;
        }
    }
}
namespace PizzaCalories;

public class Dough
{
    private const double CaloriesPerGram = 2;
    private const double MinWeight = 1;
    private const double MaxWeight = 200;
    
    private string flourType;
    private string bakingTechnique;
    private double weight;

    private readonly IReadOnlyDictionary<string, double> caloriesPerFlourType;
    private readonly IReadOnlyDictionary<string, double> caloriesPerBakingTechnique;
    
    public Dough(string flourType, string bakingTechnique, double weight)
    {
        caloriesPerFlourType = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            { "white", 1.5 },
            { "wholegrain", 1 }
        };
        
        caloriesPerBakingTechnique = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            { "crispy", 0.9 },
            { "chewy", 1.1 },
            { "homemade", 1 }
        };
        
        FlourType = flourType;
        BakingTechnique = bakingTechnique;
        Weight = weight;
    }

    public string FlourType
    {
        get => flourType;
        private set
        {
            if (!caloriesPerFlourType.ContainsKey(value))
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            
            flourType = value;
        }
    }
    
    public string BakingTechnique
    {
        get => bakingTechnique;
        private set
        {
            if (!caloriesPerBakingTechnique.ContainsKey(value))
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            
            bakingTechnique = value;
        }
    }
    
    public double Weight
    {
        get => weight;
        private set
        {
            if (value is < MinWeight or > MaxWeight)
            {
                throw new ArgumentException($"Dough weight should be in the range [{MinWeight}..{MaxWeight}].");
            }
            
            weight = value;
        }
    }

    public double GetDoughCalories
    {
        get
        {
            double flourTypeModifier = caloriesPerFlourType[FlourType];
            double bakingTechniqueModifier = caloriesPerBakingTechnique[BakingTechnique];
            
            return (CaloriesPerGram * Weight) * flourTypeModifier * bakingTechniqueModifier;
        }
    }
}
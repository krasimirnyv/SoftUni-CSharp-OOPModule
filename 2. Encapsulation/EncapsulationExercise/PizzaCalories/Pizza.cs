namespace PizzaCalories;

public class Pizza
{
    private const int MinToppingsCount = 0;
    private const int MaxToppingsCount = 10;
    private const int MinNameLength = 1;
    private const int MaxNameLength = 15;
    
    private string name;
    private List<Topping> toppings;
    
    public Pizza(string name)
    {
        Name = name;
        toppings = new List<Topping>();
    }
    
    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > MaxNameLength)
            {
                throw new ArgumentException($"Pizza name should be between {MinNameLength} and {MaxNameLength} symbols.");
            }
            
            name = value;
        }
    }

    public Dough Dough { get; set; }
    
    public void AddTopping(Topping topping)
    {
        if (toppings.Count >= MaxToppingsCount)
        {
            throw new InvalidOperationException($"Number of toppings should be in range [{MinToppingsCount}..{MaxToppingsCount}].");
        }
        
        toppings.Add(topping);
    }
    
    public override string ToString()
        => $"{Name} - {GetPizzaCalories:F2} Calories.";
    
    private double GetPizzaCalories
    {
        get
        {
           double doughCalories = Dough.GetDoughCalories;
           double toppingsCalories = toppings.Sum(t => t.GetToppingCalories);

           return doughCalories + toppingsCalories;
        }
    }
}
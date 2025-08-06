namespace Prototype;

public class Sandwich : SandwichPrototype
{
    private string _bread;
    private string _meat;
    private string _cheese;
    private string _vegetables;

    public Sandwich(string bread, string meat, string cheese, string vegetables, List<decimal> prices)
    {
        _bread = bread;
        _meat = meat;
        _cheese = cheese;
        _vegetables = vegetables;
        Prices = prices;
    }

    public List<decimal> Prices { get; set; }
    
    public override SandwichPrototype Clone()
    {
        Console.WriteLine($"Cloning sandwich with ingredients: {GetIngredients()}");
        
        return MemberwiseClone() as SandwichPrototype;
    }

    public override SandwichPrototype DeepClone()
    {
        Console.WriteLine($"Cloning sandwich with ingredients: {GetIngredients()}");

        List<decimal> prices = new();
        prices.AddRange(Prices);

        Sandwich cloned = new(_bread, _meat, _cheese, _vegetables, prices);
        
        return cloned;
    }
    
    private string GetIngredients()
        => $"{_bread}, {_meat}, {_cheese}, {_vegetables}";
    
}
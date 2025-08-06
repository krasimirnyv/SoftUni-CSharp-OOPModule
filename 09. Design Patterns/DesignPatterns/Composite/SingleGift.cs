namespace Composite;

public class SingleGift : GiftBase
{
    public SingleGift(string name, decimal price) 
        : base(name, price)
    {
    }
    
    public override decimal CalculatePrice()
    {
        Console.WriteLine($"{Name} with price: {Price}");
        return Price;
    }
}
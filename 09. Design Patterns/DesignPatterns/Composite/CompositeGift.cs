namespace Composite;

public class CompositeGift : GiftBase, IGiftOperations
{
    private List<GiftBase> _gifts;
    
    public CompositeGift(string name, decimal price) 
        : base(name, price)
    {
        _gifts = new List<GiftBase>();
    }

    public void Add(GiftBase gift)
        => _gifts.Add(gift);

    public void Remove(GiftBase gift)
        => _gifts.Remove(gift);

    public override decimal CalculatePrice()
    {
        decimal total = 0;
        Console.WriteLine($"{Name} contains the following gifts with prices:");

        foreach (GiftBase gift in _gifts)
        {
            total += gift.CalculatePrice();
        }

        return total;
    }
}
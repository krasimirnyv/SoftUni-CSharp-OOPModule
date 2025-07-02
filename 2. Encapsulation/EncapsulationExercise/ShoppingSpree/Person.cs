namespace ShoppingSpree;

public class Person
{
    private string name;
    private decimal money;
    private List<Product> bag;
    
    public Person(string name, decimal money)
    {
        Name = name;
        Money = money;
        bag = new List<Product>();
    }
    
    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be empty");
            }
            
            name = value;
        }
    }

    public decimal Money
    {
        get => money;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Money cannot be negative");
            }
            
            money = value;
        }
    }
    
    public int CountProducts
        => bag.Count;
    
    public string BuyProduct(Product product)
    {
        if (product.Cost > Money)
        {
            throw new InvalidOperationException($"{Name} can't afford {product}");
        }
        
        Money -= product.Cost;
        bag.Add(product);
        
        return $"{Name} bought {product}";
    }

    public override string ToString()
        => $"{Name} - {string.Join(", ", bag)}";
}
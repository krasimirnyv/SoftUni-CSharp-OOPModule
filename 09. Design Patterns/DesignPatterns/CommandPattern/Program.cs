namespace CommandPattern;

class Program
{
    static void Main(string[] args)
    {
        var modifyPrice = new ModifyPrice();
        var product = new Product("Laptop", 1000.00m);
        
        Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 100m));
        Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Increase, 50m));
        Execute(product, modifyPrice, new ProductCommand(product, PriceAction.Decrease, 30m));

        Console.WriteLine(product);
    }

    private static void Execute(Product product, ModifyPrice modifyPrice, ICommand command)
    {
        modifyPrice.SetCommand(command);
        modifyPrice.Invoke();
    }
}
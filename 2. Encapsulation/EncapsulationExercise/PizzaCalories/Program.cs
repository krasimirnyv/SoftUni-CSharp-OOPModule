namespace PizzaCalories;

public class Program
{
    public static void Main(string[] args)
    {
        string pizzaName = Console.ReadLine().Split()[1];
        
        string[] doughData = Console.ReadLine().Split();
        string flourType = doughData[1];
        string bakingTechnique = doughData[2];
        double doughWeight = double.Parse(doughData[3]);
        
        try
        {
            Pizza pizza = new Pizza(pizzaName);
            Dough dough = new Dough(flourType, bakingTechnique, doughWeight);

            pizza.Dough = dough;
            
            string toppingInput = default;
            while ((toppingInput = Console.ReadLine()) != "END")
            {
                string[] toppingData = toppingInput.Split();
                string toppingType = toppingData[1];
                double toppingWeight = double.Parse(toppingData[2]);
            
                Topping topping = new Topping(toppingType, toppingWeight);
                pizza.AddTopping(topping);
            }
            
            Console.WriteLine(pizza);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}
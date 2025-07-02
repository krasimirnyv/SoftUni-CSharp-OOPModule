namespace ShoppingSpree;

public class Program
{
    public static void Main(string[] args)
    {
        List<Person> people = new List<Person>();
        List<Product> products = new List<Product>();

        try
        {
            string[] humans = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            AddPersonFromData(humans, people);
        
            string[] productsData = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            AddProductFromData(productsData, products);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        

        string input = default;
        while ((input = Console.ReadLine()) != "END")
        {
            string[] tokens = input.Split();
            
            string personName = tokens[0],
                   productName = tokens[1];
            
            Person person = people.FirstOrDefault(p => p.Name == personName);
            Product product = products.FirstOrDefault(p => p.Name == productName);
            
            if (person == null || product == null)
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            try
            {
                string result = person.BuyProduct(product);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); 
            }
        }

        foreach (Person person in people)
        {
            if (person.CountProducts == 0)
            {
                Console.WriteLine($"{person.Name} - Nothing bought");
                continue;
            }
            
            Console.WriteLine(person);
        }
    }

    private static void AddPersonFromData(string[] humans, List<Person> people)
    {
        foreach (string human in humans)
        {
            string[] personData = human.Split('=');

            string name = personData[0];
            decimal money = decimal.Parse(personData[1]);
            
            Person person = new Person(name, money); 
            people.Add(person);
        }
    }
    
    private static void AddProductFromData(string[] productsData, List<Product> products)
    {
        foreach (string productTokens in productsData)
        {
            string[] productData = productTokens.Split('=');

            string name = productData[0];
            decimal cost = decimal.Parse(productData[1]);

            Product product = new Product(name, cost);
            products.Add(product);
        }
    }
}
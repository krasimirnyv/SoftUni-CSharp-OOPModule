namespace BorderControl;

public class StartUp
{
    public static void Main(string[] args)
    {
        
        HashSet<IBuyer> buyers = new HashSet<IBuyer>();
        
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string[] tokens = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string name;
            int age;
            if (tokens.Length == 4)
            {
                name = tokens[0];
                age = int.Parse(tokens[1]);
                string id = tokens[2];
                string birthdate = tokens[3];
                buyers.Add(new Citizen(name, age, id, birthdate));
            }
            else if (tokens.Length == 3)
            {
                name = tokens[0];
                age = int.Parse(tokens[1]);
                string group = tokens[2];
                buyers.Add(new Rebel(name, age, group));
            }
        }

        string input = default;
        while ((input = Console.ReadLine()) != "End")
        {
            IBuyer buyer = buyers.FirstOrDefault(b => b.Name == input);
            
            if(buyer != null) 
                buyer.BuyFood();
        }

        int totalFood = buyers.Sum(b => b.Food);
        Console.WriteLine(totalFood);
    }
}
namespace ExplicitInterfaces;

public class StartUp
{
    public static void Main(string[] args)
    {
        List<Citizen> society = new List<Citizen>();
        
        string input = default;
        while ((input = Console.ReadLine()) != "End")
        {
            string[] tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            
            string name = tokens[0];
            string country = tokens[1];
            int age = int.Parse(tokens[2]);
            
            society.Add(new Citizen(name, country, age));
        }

        foreach (Citizen citizen in society)
        {
            Console.WriteLine((citizen as IPerson).GetName());
            Console.WriteLine((citizen as IResident).GetName());
        }
    }
}
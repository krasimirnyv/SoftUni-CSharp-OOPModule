namespace AnimalFarm.Models;

public class Chicken
{
    private const int MinAge = 0;
    private const int MaxAge = 15;

    private string name;
    private int age;

    public Chicken(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name
    {
        get => name;

        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{nameof(Name)} cannot be empty.");
            }
            
            name = value;
        }
    }

    public int Age
    {
        get => age;

        private set
        {
            if (value is < MinAge or > MaxAge)
            {
                throw new ArgumentException($"{nameof(Age)} should be between {MinAge} and {MaxAge}.");
            }
            
            age = value;
        }
    }

    public double ProductPerDay 
        => CalculateProductPerDay();

    public override string ToString()
        => $"Chicken {Name} (age {Age}) can produce {ProductPerDay} eggs per day.";

    private double CalculateProductPerDay()
    {
        return Age switch
        {
            0 or 1 or 2 or 3 => 1.5,
            4 or 5 or 6 or 7 => 2,
            8 or 9 or 10 or 11 => 1,
            _ => 0.75
        };
    }
}
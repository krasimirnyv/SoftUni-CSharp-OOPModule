namespace FootballTeamGenerator.Models;

public class Player
{
    private const int MinSkill = 0;
    private const int MaxSkill = 100;
    
    private string name;
    private int endurance;
    private int sprint;
    private int dribble;
    private int passing;
    private int shooting;
    
    public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
    {
        Name = name;
        Endurance = endurance;
        Sprint = sprint;
        Dribble = dribble;
        Passing = passing;
        Shooting = shooting;
    }

    public string Name
    {
        get => name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("A name should not be empty.");
            }
            
            name = value;
        }
    }
    
    public int Endurance
    {
        get => endurance;
        private set
        {
            if (InvalidSkill(value))
            {
                throw new ArgumentException($"{nameof(Endurance)} should be between {MinSkill} and {MaxSkill}.");
            }

            endurance = value;
        }
    }
    
    public int Sprint
    {
        get => sprint;
        private set
        {
            if (InvalidSkill(value))
            {
                throw new ArgumentException($"{nameof(Sprint)} should be between {MinSkill} and {MaxSkill}.");
            }

            sprint = value;
        }
    }
    
    public int Dribble
    {
        get => dribble;
        private set
        {
            if (InvalidSkill(value))
            {
                throw new ArgumentException($"{nameof(Dribble)} should be between {MinSkill} and {MaxSkill}.");
            }

            dribble = value;
        }
    }
    
    public int Passing
    {
        get => passing;
        private set
        {
            if (InvalidSkill(value))
            {
                throw new ArgumentException($"{nameof(Passing)} should be between {MinSkill} and {MaxSkill}.");
            }

            passing = value;
        }
    }
    
    public int Shooting
    {
        get => shooting;
        private set
        {
            if (InvalidSkill(value))
            {
                throw new ArgumentException($"{nameof(Shooting)} should be between {MinSkill} and {MaxSkill}.");
            }

            shooting = value;
        }
    }
    
    public double Stats 
        => (Endurance + Sprint + Dribble + Passing + Shooting) / 5.0;
    
    private static bool InvalidSkill(int value)
    => value < MinSkill || value > MaxSkill;
    
}
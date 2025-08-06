namespace FootballTeamGenerator;

using FootballTeamGenerator.Models;

public class Program
{
    public static void Main(string[] args)
    {
        List<Team> teams = new List<Team>();
        
        string input = default;
        while ((input = Console.ReadLine()) != "END")
        {
            string[] tokens = input.Split(';');
            string command = tokens[0];
            
            try
            {
                switch (command)
                {
                    case "Add":
                        AddPlayerToTeam(tokens, teams);
                        break;
                    case "Remove":
                        RemovePlayerFromTeam(tokens, teams);
                        break;
                    case "Rating":
                        PrintTeamRating(tokens, teams);
                        break;
                    default: // Initialize Team
                        string teamName = tokens[1];
                        teams.Add(new Team(teamName));
                        break;
                }
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            
        }
    }

    private static void AddPlayerToTeam(string[] tokens, List<Team> teams)
    {
        string addPlayerToTeam = tokens[1];
        string playerName = tokens[2];
        int endurance = int.Parse(tokens[3]);
        int sprint = int.Parse(tokens[4]);
        int dribble = int.Parse(tokens[5]);
        int passing = int.Parse(tokens[6]);
        int shooting = int.Parse(tokens[7]);
                        
        Team findTeam = teams.FirstOrDefault(t => t.Name == addPlayerToTeam);
        
        ValidateTeamExistence(findTeam, addPlayerToTeam);
        
        Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
        findTeam.AddPlayer(player);
    }
    
    private static void RemovePlayerFromTeam(string[] tokens, List<Team> teams)
    {
        string removePlayerFromTeam = tokens[1];
        string playerName = tokens[2];
        
        Team findTeam = teams.FirstOrDefault(t => t.Name == removePlayerFromTeam);
       
        ValidateTeamExistence(findTeam, removePlayerFromTeam);
        findTeam.RemovePlayer(playerName);
    }
    
    private static void PrintTeamRating(string[] tokens, List<Team> teams)
    {
        string teamName = tokens[1];
        Team findTeam = teams.FirstOrDefault(t => t.Name == teamName);
        
        ValidateTeamExistence(findTeam, teamName);

        Console.WriteLine($"{findTeam.Name} - {findTeam.Rating}");
    }
    
    private static void ValidateTeamExistence(Team findTeam, string teamName)
    {
        if (findTeam == null)
        {
            throw new ArgumentException($"Team {teamName} does not exist.");
        }
    }
}
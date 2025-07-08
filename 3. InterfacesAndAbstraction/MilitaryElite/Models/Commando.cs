using System.Text;
using MilitaryElite.Enums;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Models;

public class Commando : SpecialisedSoldier, ICommando
{
    public Commando(string id, string firstName, string lastName, decimal salary, Corps corps, IReadOnlyCollection<IMission> missions) 
        : base(id, firstName, lastName, salary, corps)
    {
        Missions = missions;
    }

    public IReadOnlyCollection<IMission> Missions { get; private set; }
    
    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.Append(base.ToString());
        result.Append("Missions:");
        
        foreach (IMission mission in Missions)
        {
            result.AppendLine();
            result.Append($"  {mission.ToString()}");
        }
        
        return result.ToString();
    }
}
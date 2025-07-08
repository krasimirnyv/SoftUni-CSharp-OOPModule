using System.Text;
using MilitaryElite.Enums;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Models;

public class Engineer : SpecialisedSoldier, IEngineer
{
    public Engineer(string id, string firstName, string lastName, decimal salary, Corps corps, IReadOnlyCollection<IRepair> repairs) 
        : base(id, firstName, lastName, salary, corps)
    {
        Repairs = repairs;
    }

    public IReadOnlyCollection<IRepair> Repairs { get; private set; }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.Append(base.ToString());
        result.Append("Repairs:");
        
        foreach (IRepair repair in Repairs)
        {
            result.AppendLine();
            result.Append($"  {repair.ToString()}");
        }
        
        return result.ToString();
    }
}
using System.Text;
using MilitaryElite.Enums;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Models;

public abstract class SpecialisedSoldier : Private, ISpecialisedSoldier
{
    protected SpecialisedSoldier(string id, string firstName, string lastName, decimal salary, Corps corps) 
        : base(id, firstName, lastName, salary)
    {
        Corps = corps;
    }

    public Corps Corps { get; private set; }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        
        result.AppendLine(base.ToString());
        result.AppendLine($"Corps: {Corps}");
        return result.ToString();
    }
}
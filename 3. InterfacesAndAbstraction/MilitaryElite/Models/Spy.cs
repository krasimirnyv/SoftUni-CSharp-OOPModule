using System.Text;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Models;

public class Spy : Soldier, ISpy
{
    public Spy(string id, string firstName, string lastName, int codeNumber) 
        : base(id, firstName, lastName)
    {
        CodeNumber = codeNumber;
    }

    public int CodeNumber { get; private set; }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        
        result.AppendLine(base.ToString());
        result.Append($"Code Number: {CodeNumber}");
        return result.ToString();
    }
}
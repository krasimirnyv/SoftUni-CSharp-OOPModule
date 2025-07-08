using System.Text;
using MilitaryElite.Models.Interfaces;

namespace MilitaryElite.Models;

public class LieutenantGeneral : Private, ILieutenantGeneral
{
    public LieutenantGeneral(string id, string firstName, string lastName, decimal salary, IReadOnlyCollection<IPrivate> privates)
        : base(id, firstName, lastName, salary)
    {
        Privates = privates;
    }

    public IReadOnlyCollection<IPrivate> Privates { get; private set; }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(base.ToString());
        result.Append("Privates:");

        foreach (IPrivate currentPrivate in Privates)
        {
            result.AppendLine();
            result.Append($"  {currentPrivate.ToString()}");
        }
        
        return result.ToString();
    }
}
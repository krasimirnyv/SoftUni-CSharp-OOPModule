namespace MilitaryElite.Models.Interfaces;

public interface ILieutenantGeneral : IPrivate
{
    IReadOnlyCollection<IPrivate> Privates { get; }
}
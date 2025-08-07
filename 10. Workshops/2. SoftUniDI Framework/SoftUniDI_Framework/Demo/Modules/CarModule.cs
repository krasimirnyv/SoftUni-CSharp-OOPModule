using Demo.Interfaces;
using Demo.Models;
using SoftUniDI_Framework;

namespace Demo.Modules;

public class CarModule : AbstractModule
{
    public override void Configure()
    {
        CreateMapping<ICar, Car>();
        CreateMapping<IEngine, Engine>();
        CreateMapping<IFuel, DieselFuel>();
        CreateMapping<ITyres, Tyres>();
        CreateMapping<ISuspension, Suspension>();
    }
}
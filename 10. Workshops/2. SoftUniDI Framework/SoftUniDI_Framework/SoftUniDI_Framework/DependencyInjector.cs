using SoftUniDI_Framework.Interfaces;

namespace SoftUniDI_Framework;

public class DependencyInjector
{
    public static Injector CreateInjector(IModule module)
    {
        module.Configure();
        return new Injector(module);
    }
}
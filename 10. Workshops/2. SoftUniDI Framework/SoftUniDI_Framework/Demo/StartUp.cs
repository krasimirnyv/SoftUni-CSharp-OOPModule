using Demo.Interfaces;
using Demo.Models;
using Demo.Modules;
using SoftUniDI_Framework;

namespace Demo;

public class StartUp
{
    public static void Main()
    {
        Injector injector = DependencyInjector.CreateInjector(new CarModule());
        ICar car = injector.Inject<Car>();
        car.Drive();
    }
}
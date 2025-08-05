namespace Facade;

class Program
{
    static void Main(string[] args)
    {
        Car car = new CarBuilderFacade()
            .Info
                .WithType("BMW")
                .WithColor("Deep Purple")
                .WithNumberOfDoors(5)
            .Built
                .InCity("Leipzig")
                .AtAddress("Karl-Heine-Straße 99")
            .Build();
            
        Console.WriteLine(car);
    }
}
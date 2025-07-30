using WildFarm.Core.Interfaces;
using WildFarm.Factories.Interfaces;
using WildFarm.IO.Interfaces;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core;

public class Engine : IEngine
{
    private readonly IReader reader;
    private readonly IWriter writer;
    
    private readonly IAnimalFactory animalFactory;
    private readonly IFoodFactory foodFactory;
    
    private readonly ICollection<IAnimal> animals;
    
    public Engine(IReader reader, IWriter writer,
        IAnimalFactory animalFactory, IFoodFactory foodFactory)
    {
        this.reader = reader;
        this.writer = writer;
        
        this.animalFactory = animalFactory;
        this.foodFactory = foodFactory;
        
        animals = new List<IAnimal>();
    }
    
    public void Run()
    {
        string input = default;
        while ((input = reader.ReadLine()) != "End")
        {
            IAnimal animal = null;
            
            try
            {
                animal = CreateAnimal(input);
                IFood food = CreateFood();
                
                writer.WriteLine(animal.ProduceSound());
                animal.Eat(food);
            }
            catch (ArgumentException exception)
            {
                writer.WriteLine(exception.Message);
            }
            
            animals.Add(animal);

        }

        foreach (IAnimal animal in animals)
        {
            writer.WriteLine(animal);
        }
    }

    private IAnimal CreateAnimal(string input)
    {
        string[] animalArgs = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        IAnimal animal = animalFactory.CreateAnimal(animalArgs);

        return animal;
    }
    
    private IFood CreateFood()
    {
        string[] foodArgs = reader.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        string foodType = foodArgs[0];
        int foodQuantity = int.Parse(foodArgs[1]);

        IFood food = foodFactory.CreateFood(foodType, foodQuantity);

        return food;
    }
}
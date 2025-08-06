using WildFarm.Factories.Interfaces;
using WildFarm.Models.Animals;
using WildFarm.Models.Interfaces;

namespace WildFarm.Factories;

public class AnimalFactory : IAnimalFactory
{
    public IAnimal CreateAnimal(string[] animalTokens)
    {
        string type = animalTokens[0];
        
        string name = animalTokens[1];
        double weight = double.Parse(animalTokens[2]);

        switch (type)
        {
            case "Owl":
                double wingSize = double.Parse(animalTokens[3]);
                return new Owl(name, weight, wingSize);
            case "Hen":
                wingSize = double.Parse(animalTokens[3]);
                return new Hen(name, weight, wingSize);
            case "Mouse":
                string livingRegion = animalTokens[3];
                return new Mouse(name, weight, livingRegion);
            case "Dog":
                livingRegion = animalTokens[3];
                return new Dog(name, weight, livingRegion);
            case "Cat":
                livingRegion = animalTokens[3];
                string breed = animalTokens[4];
                return new Cat(name, weight, livingRegion, breed);
            case "Tiger":
                livingRegion = animalTokens[3];
                breed = animalTokens[4];
                return new Tiger(name, weight, livingRegion, breed);
            default:
                throw new ArgumentException("Invalid animal type!");
        }
    }
}
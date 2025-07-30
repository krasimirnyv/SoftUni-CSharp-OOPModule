namespace Prototype;

class Program
{
    static void Main(string[] args)
    {
        SandwichMenu sandwichMenu = new SandwichMenu();

        sandwichMenu["BLT"] = new Sandwich("Wheat", "Bacon", "", "Lettuce, Tomato", new List<decimal> { 5.99m, 6.49m, 6.99m });
        sandwichMenu["PB&J"] = new Sandwich("White", "", "", "Peanut Butter, Jelly", new List<decimal> { 2.99m, 3.49m });
        
        Sandwich sandwich1 = sandwichMenu["BLT"].Clone() as Sandwich;
        Sandwich sandwich2 = sandwichMenu["PB&J"].DeepClone() as Sandwich;
    }
}
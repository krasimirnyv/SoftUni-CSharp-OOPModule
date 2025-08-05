namespace TemplatePattern;

public class TwelveGrain : Bread
{
    public override void MixIngredients()
        => Console.WriteLine($"Gathering Ingredients for {GetType().Name} Bread");

    public override void Bake() 
        => Console.WriteLine($"Baking the {GetType().Name} Bread (30 minutes).");
}
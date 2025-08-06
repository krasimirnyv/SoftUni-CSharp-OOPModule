namespace Restaurant;

public class Cake : Dessert
{
    private const double DefaultCakeGrams = 250;
    private const double DefaultCalories = 1000;
    private const decimal DefaultCakePrice = 5M;

    public Cake(string name) 
        : base(name, DefaultCakePrice, DefaultCakeGrams, DefaultCalories)
    {
    }
}
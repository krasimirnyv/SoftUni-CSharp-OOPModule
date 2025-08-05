namespace TemplatePattern;

class Program
{
    static void Main(string[] args)
    {
        SourDough sourDough = new SourDough();
        sourDough.Make();
        
        TwelveGrain twelveGrain = new TwelveGrain();
        twelveGrain.Make();
        
        WholeWheat wholeWheat = new WholeWheat();
        wholeWheat.Make();
    }
}
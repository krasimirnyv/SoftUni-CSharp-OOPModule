namespace CommandPattern;

public class ProductCommand : ICommand
{
    private readonly Product _product;
    private readonly PriceAction _priceAction;
    private readonly decimal _amount;

    public ProductCommand(Product product, PriceAction priceAction, decimal amount)
    {
        _product = product;
        _priceAction = priceAction;
        _amount = amount;
    }
    
    public void ExecuteAction()
    {
        if (_priceAction == PriceAction.Increase)
            _product.IncreasePrice(_amount);
        else //_priceAction == PriceAction.Decrease
            _product.DecreasePrice(_amount);
    }
}
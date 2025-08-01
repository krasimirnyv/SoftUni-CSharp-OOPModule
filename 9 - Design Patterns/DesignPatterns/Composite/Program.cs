﻿namespace Composite;

class Program
{
    static void Main(string[] args)
    {
        var phone = new SingleGift("Phone", 256);
        phone.CalculatePrice();
        Console.WriteLine();

        var rootBox = new CompositeGift("RootBox", 0);
        var truckToy = new SingleGift("TruckToy", 289);
        var plainToy = new SingleGift("PlainToy", 587);
        rootBox.Add(truckToy);
        rootBox.Add(plainToy);

        var childBox = new CompositeGift("ChildBox", 0);
        var soldierToy = new SingleGift("SoldierToy", 200);
        childBox.Add(soldierToy);
        rootBox.Add(childBox);

        Console.WriteLine($"Total price of this composite present is: {rootBox.CalculatePrice()}");
    }
}
namespace Facade;

public class Car
{
    public string Type { get; set; }
    public string Color { get; set; }
    public int NumberOfDoors { get; set; }

    public string City { get; set; }
    public string Address { get; set; }

    public override string ToString()
    => $"Car Type: {Type}, Color: {Color}, Doors: {NumberOfDoors}, Manufactured in {City}, at address: {Address}.";
}
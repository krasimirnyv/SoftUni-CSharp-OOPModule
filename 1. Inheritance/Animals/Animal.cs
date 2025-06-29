using System;
using System.Text;

namespace Animals;

public abstract class Animal
{
    private string _name;
    private int _age;
    private string _gender;

    protected Animal(string name, int age, string gender)
    {
        Name = name;
        Age = age;
        Gender = gender;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid input!");
            }
            
            _name = value;
        }
    }

    public int Age 
    { 
        get => _age;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Invalid input!");
            }
            
            _age = value;
        } 
    }
    public string Gender 
    {
        get => _gender;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid input!");
            }
            
            _gender = value;
        }
    }

    public abstract string ProduceSound();

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();
        result.AppendLine(this.GetType().Name);
        result.AppendLine($"{Name} {Age} {Gender}");
        result.Append(ProduceSound());
        
        return result.ToString();
    }
}
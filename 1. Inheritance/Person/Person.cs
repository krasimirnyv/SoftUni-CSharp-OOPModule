using System.Text;

namespace Person;

public class Person
{
    private string _name;
    private int _age;

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public virtual int Age
    {
        get => _age;
        set
        {
            if (value > 0)
            {
                _age = value;
            }
        }
    }

    public override string ToString()
    {
        return $"{this.GetType().Name} -> Name: {Name}, Age: {Age}";
    }
}
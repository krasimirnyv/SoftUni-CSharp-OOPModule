using System;
using ExtendedDatabase;

namespace DatabaseExtended.Tests;

using NUnit.Framework;

[TestFixture]
public class ExtendedDatabaseTests
{
    private Person[] people;
    
    [Test]
    public void Constructor_ShouldInitializePerson()
    {
        var id = 1;
        var userName = "Krasi";
        
        var person = new Person(id, userName);
        
        Assert.That(person.Id, Is.EqualTo(id));
        Assert.That(person.UserName, Is.EqualTo(userName));
    }

    [SetUp]
    public void SetUp()
    {
        Person person = new Person(1, "Krasi");
        Person person2 = new Person(2, "Pesho");
        Person person3 = new Person(3, "Gosho");
        
        people = new Person[] { person, person2, person3 };
    }

    [Test]
    public void Constructor_ShouldInitializeDatabase()
    {
        var database = new Database(people);
        
        Assert.That(database.Count, Is.EqualTo(people.Length));
    }
    
    [Test]
    public void Constructor_ShouldThrowException_WhenMoreThan16People()
    {
        Person[] moreThan16People = new Person[17];
        for (int i = 0; i < 17; i++)
        {
            moreThan16People[i] = new Person(i, "User" + i);
        }
        
        Assert.Throws<ArgumentException>(() => new Database(moreThan16People), "Provided data length should be in range [0..16]!");
    }
    
    [Test]
    public void Add_ShouldThrowException_WhenAddingMoreThan16People()
    {
        var database = new Database(people);
        
        for (int i = 4; i <= 16; i++)
        {
            database.Add(new Person(i, "User" + i));
        }
        
        Assert.Throws<InvalidOperationException>(() => database.Add(new Person(17, "User17")), "Array's capacity must be exactly 16 integers!");
    }

    [Test]
    public void Add_ShouldThrowException_WhenAddingPersonWithExistingUsername()
    {
        var database = new Database(people);
        
        var newPerson = new Person(4, "Krasi"); // Existing username
        
        Assert.Throws<InvalidOperationException>(() => database.Add(newPerson), "There is already user with this username!");
    }

    [Test]
    public void Add_ShouldThrowException_WhenAddingPersonWithExistingId()
    {
        var database = new Database(people);
        
        var newPerson = new Person(1, "Maria"); // Existing id
        
        Assert.Throws<InvalidOperationException>(() => database.Add(newPerson), "There is already user with this Id!");
    }
    
    [Test]
    public void Remove_ShouldRemovePersonCorrectly()
    {
        var database = new Database(people);
        var initialCount = database.Count;
        database.Remove();
        
        Assert.That(database.Count, Is.EqualTo(initialCount - 1));
    }

    [Test]
    public void Remove_ShouldThrowException_WhenRemovingFromEmptyDatabase()
    {
        var database = new Database();
        
        Assert.Throws<InvalidOperationException>(() => database.Remove());
    }
    
    [Test]
    public void FindByUsername_ShouldReturnCorrectPerson()
    {
        var person = new Person(1, "Krasi");
        var database = new Database(person);
        var findByUsername = database.FindByUsername("Krasi");
        
        Assert.That(findByUsername.UserName, Is.EqualTo(person.UserName));
        Assert.That(findByUsername.Id, Is.EqualTo(person.Id));
    }
    
    [Test]
    public void FindByUsername_ShouldThrowException_WhenUsernameDoesNotExist()
    {
        var person = new Person(1, "Krasi");
        var database = new Database(person);
        
        Assert.Throws<InvalidOperationException>(() => database.FindByUsername("NonExistentUser"), "No user is present by this username!");
    }

    [Test]
    public void FindByUsername_ShouldThrowException_WhenUsernameIsNull()
    {
        var person = new Person(1, "Krasi");
        var database = new Database(person);
        
        Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null), "Username parameter is null!");
    }

    [Test]
    public void FindById_ShouldReturnCorrectPerson()
    {
        var person = new Person(1, "Krasi");
        var database = new Database(person);
        var findById = database.FindById(1);
        
        Assert.That(findById.UserName, Is.EqualTo(person.UserName));
        Assert.That(findById.Id, Is.EqualTo(person.Id));
    }

    [Test]
    public void FindById_ShouldThrowException_WhenIdDoesNotExist()
    {
        var person = new Person(1, "Krasi");
        var database = new Database(person);
        
        Assert.Throws<InvalidOperationException>(() => database.FindById(999), "No user is present by this ID!");
    }

    [Test]
    public void FindById_ShouldThrowException_WhenIdIsNegative()
    {
        var person = new Person(1, "Krasi");
        var database = new Database(person);
        
        Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1), "Id should be a positive number!");
    }
}

﻿using CollectionHierarchy.Models;

namespace CollectionHierarchy;

public class StartUp
{
    public static void Main(string[] args)
    {
        Dictionary<string, List<int>> addedIndexes = new Dictionary<string, List<int>>()
        {
            { "AddCollection", new List<int>() },
            { "AddRemoveCollection", new List<int>() },
            { "MyList", new List<int>() }
        };

        Dictionary<string, List<string>> removedItems = new Dictionary<string, List<string>>()
        {
            { "AddCollection", new List<string>() },
            { "AddRemoveCollection", new List<string>() },
            { "MyList", new List<string>() }
        };
        
        IAddCollection addCollection = new AddCollection();
        IAddRemoveCollection addRemoveCollection = new AddRemoveCollection();
        IMyList myList = new MyList();
        
        string[] items = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string item in items)
        {
            addedIndexes["AddCollection"].Add(addCollection.Add(item));
            
            addedIndexes["AddRemoveCollection"].Add(addRemoveCollection.Add(item));
            
            addedIndexes["MyList"].Add(myList.Add(item));
        }
        
        
        int removeCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < removeCount; i++)
        {
            removedItems["AddRemoveCollection"].Add(addRemoveCollection.Remove());
            
            removedItems["MyList"].Add(myList.Remove());
        }
        
        Console.WriteLine(string.Join(" ", addedIndexes["AddCollection"]));
        Console.WriteLine(string.Join(" ", addedIndexes["AddRemoveCollection"]));
        Console.WriteLine(string.Join(" ", addedIndexes["MyList"]));
        
        Console.WriteLine(string.Join(" ", removedItems["AddRemoveCollection"]));
        Console.WriteLine(string.Join(" ", removedItems["MyList"]));
    }
}
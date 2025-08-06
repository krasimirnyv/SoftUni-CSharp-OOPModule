namespace CollectionHierarchy.Models;

public class MyList : IMyList
{
    private const int AddIndex = 0;
    private const int RemoveIndex = 0;
    
    private readonly List<string> data;
    
    public MyList()
    {
        data = new List<string>();
    }
    
    public int Add(string item)
    {
        data.Insert(AddIndex, item);
        return AddIndex; // Always returns 0 since we add at the start
    }

    public string Remove()
    {
        string item = data[RemoveIndex];
        data.RemoveAt(RemoveIndex); // Remove the first item
        return item;
    }

    public int Used => data.Count;
}
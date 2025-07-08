namespace CollectionHierarchy.Models;

public class AddRemoveCollection : IAddRemoveCollection
{
    private const int AddIndex = 0;
    
    private readonly List<string> data;
    
    public AddRemoveCollection()
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
        string item = null;

        if (data.Count > 0)
        {
            item = data[^1];
            data.RemoveAt(data.Count - 1); // Remove the last item
        }
        
        return item;
    }
}
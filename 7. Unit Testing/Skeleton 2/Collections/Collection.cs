using System;
using System.Text;

namespace Collections;

public class Collection<T>
{
    private const int InitialCapacity = 16;
    
    private T[] _items;

    public Collection(params T[] items)
    {
        int capacity = Math.Max(2 * items.Length, InitialCapacity);
        _items = new T[capacity];
        
        for (int i = 0; i < items.Length; i++)
            _items[i] = items[i];
        
        Count = items.Length;
    }

    public int Capacity => _items.Length;
    public int Count { get; private set; }
        
    public void Add(T item)
    {
        EnsureCapacity();
        _items[Count] = item;
        Count++;
    }

    public void AddRange(params T[] items)
    {
        foreach (T item in items)
            Add(item);
    }

    private void EnsureCapacity()
    {
        if (Count == Capacity)
        {
            // Grow the capacity 2 times and move the items
            T[] oldItems = _items;
            _items = new T[2 * oldItems.Length];
            
            for (int i = 0; i < Count; i++)
                _items[i] = oldItems[i];
        }
    }

    public T this[int index]
    {
        get
        {
            CheckRange(index, nameof(index), minValue: 0, maxValue: Count-1);
            return _items[index];
        }
        set
        {
            CheckRange(index, nameof(index), minValue: 0, maxValue: Count-1);
            _items[index] = value;
        }
    }

    public void InsertAt(int index, T item)
    {
        CheckRange(index, nameof(index), minValue: 0, maxValue: Count);
        EnsureCapacity();
        
        for (int i = Count-1; i >= index; i--)
            _items[i + 1] = _items[i];
        
        _items[index] = item;
        Count++;
    }

    private void CheckRange(int index, string paramName, int minValue, int maxValue)
    {
        if ((index < minValue) || (index > maxValue))
        {
            string errMsg = $"Parameter should be in the range [{minValue}...{maxValue}]";
            throw new ArgumentOutOfRangeException(paramName, index, errMsg);
        }
    }

    public void Exchange(int index1, int index2)
    {
        CheckRange(index1, nameof(index1), minValue: 0, maxValue: Count - 1);
        CheckRange(index2, nameof(index2), minValue: 0, maxValue: Count - 1);
        (_items[index1], _items[index2]) = (_items[index2], _items[index1]);
    }

    public T RemoveAt(int index)
    {
        CheckRange(index, nameof(index), minValue: 0, maxValue: Count - 1);
        T removedItem = _items[index];
        
        for (int i = index+1; i < Count; i++)
            _items[i - 1] = _items[i];
        
        Count--;
        return removedItem;
    }

    public void Clear()
        => Count = 0;

    public override string ToString()
    {
        StringBuilder result = new StringBuilder("[");
        
        for (int i = 0; i < Count; i++)
        {
            if (i > 0)
                result.Append(", ");
            result.Append(_items[i]);
        }
        
        result.Append("]");
        return result.ToString();
    }
}
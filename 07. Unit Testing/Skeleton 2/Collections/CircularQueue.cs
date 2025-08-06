using System;

namespace Collections;

public class CircularQueue<T>
{
    private T[] _elements;

    private const int InitialCapacity = 8;

    public CircularQueue(int capacity = InitialCapacity)
    {
        _elements = new T[capacity];
        StartIndex = 0;
        EndIndex = 0;
    }
        
    public int Count { get; private set; }
    public int Capacity => _elements.Length;
    public int StartIndex { get; private set; }
    public int EndIndex { get; private set; }

    public void Enqueue(T element)
    {
        if (Count >= _elements.Length)
            Grow();
        
        _elements[EndIndex] = element;
        EndIndex = (EndIndex + 1) % _elements.Length;
        Count++;
    }

    private void Grow()
    {
        T[] newElements = new T[2 * _elements.Length];
        CopyAllElementsTo(newElements);
        _elements = newElements;
        StartIndex = 0;
        EndIndex = Count;
    }

    private void CopyAllElementsTo(T[] resultArr)
    {
        int sourceIndex = StartIndex;
        for (int destIndex = 0; destIndex < Count; destIndex++)
        {
            resultArr[destIndex] = _elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % _elements.Length;
        }
    }

    public T Dequeue()
    {
        if (Count == 0)
            throw new InvalidOperationException("The queue is empty!");

        T result = _elements[StartIndex];
        StartIndex = (StartIndex + 1) % _elements.Length;
        Count--;
        return result;
    }

    public T Peek()
    {
        if (Count == 0)
            throw new InvalidOperationException("The queue is empty!");

        T result = _elements[StartIndex];
        return result;
    }

    public T[] ToArray()
    {
        T[] resultArray = new T[Count];
        CopyAllElementsTo(resultArray);
        return resultArray;
    }

    public override string ToString()
        => $"[{string.Join(", ", ToArray())}]";
}
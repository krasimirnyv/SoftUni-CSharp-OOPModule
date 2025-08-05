using System;
using System.Linq;
using NUnit.Framework;

namespace Collections.Tests;

[TestFixture]
public class CircularQueueTests
{
    private const int InitialCapacity = 8;

    [Test]
    public void ConstructorDefault_ShouldInitializeWithDefaultCapacity()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        
        Assert.That(queue.ToString() == "[]");
        Assert.AreEqual(0, queue.Count);
        Assert.AreEqual(InitialCapacity, queue.Capacity);
        Assert.AreEqual(0, queue.StartIndex);
        Assert.AreEqual(0, queue.EndIndex);
    }
    
    [Test]
    public void ConstructorWithCapacity_ShouldInitializeWithGivenCapacity()
    {
        int capacity = 16;
        CircularQueue<int> queue = new CircularQueue<int>(capacity);
        
        Assert.That(queue.ToString() == "[]");
        Assert.AreEqual(0, queue.Count);
        Assert.AreEqual(capacity, queue.Capacity);
        Assert.AreEqual(0, queue.StartIndex);
        Assert.AreEqual(0, queue.EndIndex);
    }
    
    [Test]
    public void Enqueue_ShouldAddElementsToQueue()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        
        queue.Enqueue(1);
        Assert.That(queue.ToString() == "[1]");
        Assert.AreEqual(1, queue.Count);
        Assert.That(queue.Capacity >= queue.Count);
        Assert.AreEqual(InitialCapacity, queue.Capacity);
        Assert.AreEqual(0, queue.StartIndex);
        Assert.AreEqual(1, queue.EndIndex);
        
        queue.Enqueue(2);
        Assert.That(queue.ToString() == "[1, 2]");
        Assert.AreEqual(2, queue.Count);
        Assert.That(queue.Capacity >= queue.Count);
        Assert.AreEqual(InitialCapacity, queue.Capacity);
        Assert.AreEqual(0, queue.StartIndex);
        Assert.AreEqual(2, queue.EndIndex);
    }

    [Test]
    public void Enqueue_ShouldGrowQueueWhenCapacityIsReached()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        for (int i = 0; i <= InitialCapacity; i++)
        {
            queue.Enqueue(i);
        }
        
        Assert.That(queue.ToString() == "[0, 1, 2, 3, 4, 5, 6, 7, 8]");
        Assert.That(InitialCapacity * 2, Is.EqualTo(queue.Capacity));
        Assert.AreEqual(InitialCapacity + 1, queue.Count);
        Assert.AreEqual(0, queue.StartIndex);
        Assert.AreEqual(9, queue.EndIndex);
    }
    
    [Test]
    public void Dequeue_ShouldRemoveAndReturnFirstElement()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        
        int first = queue.Dequeue();
        
        Assert.AreEqual(1, first);
        Assert.That(queue.ToString() == "[2, 3]");
        Assert.AreEqual(2, queue.Count);
        Assert.AreEqual(InitialCapacity, queue.Capacity);
        Assert.AreEqual(1, queue.StartIndex);
        Assert.AreEqual(3, queue.EndIndex);
    }

    [Test]
    public void Dequeue_ShouldThrowExceptionWhenQueueIsEmpty()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        Assert.That(ex.Message, Is.EqualTo("The queue is empty!"));
    }

    [Test]
    public void Peek_ShouldReturnFirstElementWithoutRemovingIt()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        
        int first = queue.Peek();
        
        Assert.AreEqual(1, first);
        Assert.That(queue.ToString() == "[1, 2, 3]");
        Assert.AreEqual(3, queue.Count);
        Assert.AreEqual(InitialCapacity, queue.Capacity);
        Assert.AreEqual(0, queue.StartIndex);
        Assert.AreEqual(3, queue.EndIndex);
    }

    [Test]
    public void Peek_ShouldThrowExceptionWhenQueueIsEmpty()
    {
        CircularQueue<int> queue = new CircularQueue<int>();

        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => queue.Peek());
        Assert.That(ex.Message, Is.EqualTo("The queue is empty!"));
    }

    [Test]
    public void ToArray_ShouldReturnArray()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        
        int[] array = queue.ToArray();
        
        Assert.That(array, Is.EqualTo(new[] { 1, 2, 3 }));
        Assert.That(queue.ToString() == "[1, 2, 3]");
        Assert.AreEqual(queue.Count, array.Length);
        CollectionAssert.AreEqual(queue.ToArray(), array);
        CollectionAssert.AreEqual(new int[] { 1, 2, 3 }, array);
    }
    
    [Test]
    public void ToString_ShouldReturnStringRepresentation()
    {
        CircularQueue<int> queue = new CircularQueue<int>();
        Assert.That(queue.ToString() == "[]");
        
        queue.Enqueue(1);
        Assert.That(queue.ToString() == "[1]");
        
        queue.Enqueue(2);
        Assert.That(queue.ToString() == "[1, 2]");
        
        queue.Enqueue(3);
        Assert.That(queue.ToString() == "[1, 2, 3]");
        
        queue.Dequeue();
        Assert.That(queue.ToString() == "[2, 3]");
    }
    
    [Test]
    public void EnqueueDequeue_WithRangeCross_ShouldWorkCorrectly()
    {
        CircularQueue<int> queue = new CircularQueue<int>(5);
        
        for (int i = 0; i < 5; i++)
            queue.Enqueue(i);
        
        Assert.That(queue.ToString() == "[0, 1, 2, 3, 4]");

        int firstElement = queue.Dequeue();
        Assert.That(firstElement, Is.EqualTo(0));
        
        int secondElement = queue.Dequeue();
        Assert.That(secondElement, Is.EqualTo(1));
        
        Assert.That(queue.ToString() == "[2, 3, 4]");
        
        for (int i = 0; i < 2; i++)
            queue.Dequeue();
        
        Assert.That(queue.ToString() == "[4]");
        
        queue.Enqueue(5);
        Assert.That(queue.ToString() == "[4, 5]");
        
        queue.Enqueue(6);
        Assert.That(queue.ToString() == "[4, 5, 6]");
        
        Assert.That(queue.Count == 3);
        Assert.That(queue.Capacity == 5);
        Assert.That(queue.StartIndex > queue.EndIndex);
    }

    [Test]
    public void PerformanceCircularQueue_MultipleOperations_ShouldWorkCorrectly()
    {
        const int initialCapacity = 2;
        const int iterationsCount = 500;
        
        CircularQueue<int> queue = new CircularQueue<int>(initialCapacity);
        
        int addedCount = 0;
        int removedCount = 0;
        int counter = 0;

        for (int i = 0; i < iterationsCount; i++)
        {
            queue.Enqueue(++counter);
            addedCount++;
            Assert.That(queue.Count == addedCount - removedCount);
            
            queue.Enqueue(++counter);
            addedCount++;
            Assert.That(queue.Count == addedCount - removedCount);
            
            int firstElement = queue.Peek();
            Assert.That(firstElement == removedCount + 1);
            
            int removedElement = queue.Dequeue();
            removedCount++;
            
            Assert.That(removedElement == removedCount);
            Assert.That(queue.Count == addedCount - removedCount);

            int[] expectedElements = Enumerable.Range(
                removedCount + 1, addedCount - removedCount).ToArray();

            string expectedString = $"[{string.Join(", ", expectedElements)}]";
            
            int[] queueArray = queue.ToArray();
            string queueString = queue.ToString();
            
            CollectionAssert.AreEqual(expectedElements, queueArray);
            Assert.AreEqual(expectedString, queueString);
            
            Assert.That(queue.Capacity >= queue.Count);
        }
        
        Assert.That(queue.Capacity > initialCapacity);
    }

    [Test]
    public void PerformanceCircularQueue_1MillionItems_ShouldWorkCorrectly()
    {
        const int iterationsCount = 1_000_000;
        CircularQueue<int> queue = new CircularQueue<int>();
        
        int addedCount = 0;
        int removedCount = 0;
        int counter = 0;

        for (int i = 0; i < iterationsCount; i++)
        {
            queue.Enqueue(++counter);
            addedCount++;
            
            queue.Enqueue(++counter);
            addedCount++;

            queue.Dequeue();
            removedCount++;
        }
        
        Assert.That(queue.Count == addedCount - removedCount);
        Assert.That(queue.Capacity >= queue.Count);
    }
}
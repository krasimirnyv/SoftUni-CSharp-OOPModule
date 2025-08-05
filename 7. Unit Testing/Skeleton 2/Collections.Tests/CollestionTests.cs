using System;
using NUnit.Framework;

namespace Collections.Tests;

[TestFixture]
public class CollectionTests
{
    private const int InitialCapacity = 16;

    [Test]
    public void Constructor_WithSingleItem_ShouldInitializeCorrectly()
    {
        Collection<int> collection = new Collection<int>(5);

        Assert.AreEqual(1, collection.Count);
        Assert.That(collection.Capacity >= InitialCapacity);
        Assert.AreEqual("[5]", collection.ToString());
    }

    [Test]
    public void Constructor_WithMultipleItems_ShouldInitializeCorrectly()
    {
        Collection<string> collection = new Collection<string>("a", "b", "c");

        Assert.AreEqual(3, collection.Count);
        Assert.That(collection.Capacity >= 6); // 2 * 3
        Assert.AreEqual("[a, b, c]", collection.ToString());
    }

    [Test]
    public void Add_ShouldAddSingleItem()
    {
        Collection<int> collection = new Collection<int>();
        collection.Add(42);

        Assert.AreEqual(1, collection.Count);
        Assert.That(collection.Capacity >= collection.Count);
        Assert.AreEqual("[42]", collection.ToString());
    }

    [Test]
    public void AddRange_ShouldAddMultipleItems()
    {
        Collection<string> collection = new Collection<string>();
        collection.AddRange("x", "y", "z");

        Assert.AreEqual(3, collection.Count);
        Assert.AreEqual("[x, y, z]", collection.ToString());
    }

    [Test]
    public void AddRange_ShouldGrowWhenExceedsCapacity()
    {
        Collection<int> collection = new Collection<int>();
        for (int i = 0; i < InitialCapacity + 1; i++)
            collection.Add(i);

        Assert.That(collection.Capacity >= InitialCapacity * 2);
        Assert.AreEqual(InitialCapacity + 1, collection.Count);
    }

    [Test]
    public void IndexerGet_ShouldReturnCorrectItem()
    {
        Collection<string> collection = new Collection<string>("one", "two");

        Assert.AreEqual("two", collection[1]);
    }

    [Test]
    public void IndexerSet_ShouldUpdateCorrectItem()
    {
        Collection<int> collection = new Collection<int>(1, 2, 3);
        collection[1] = 42;

        Assert.AreEqual(42, collection[1]);
        Assert.AreEqual("[1, 42, 3]", collection.ToString());
    }

    [Test]
    public void Indexer_ShouldThrowExceptionForInvalidIndex()
    {
        Collection<int> collection = new Collection<int>(1, 2, 3);

        int invalidIndex1 = 3;
        int invalidIndex2 = -1;
        int min = 0;
        int max = collection.Count - 1;
        
        string expectedMessage = $"Parameter should be in the range [{min}...{max}]";

        ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => { var _ = collection[invalidIndex1]; });
        ArgumentOutOfRangeException ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => collection[invalidIndex2] = 5);
        
        StringAssert.Contains(expectedMessage, ex.Message);
        StringAssert.Contains(expectedMessage, ex2.Message);

        Assert.AreEqual("index", ex.ParamName);
        Assert.AreEqual(invalidIndex1, ex.ActualValue);

        Assert.AreEqual("index", ex2.ParamName);
        Assert.AreEqual(invalidIndex2, ex2.ActualValue);
    }

    [Test]
    public void InsertAt_ShouldInsertCorrectly()
    {
        Collection<string> collection = new Collection<string>("a", "c");
        collection.InsertAt(1, "b");

        Assert.AreEqual("[a, b, c]", collection.ToString());
        Assert.AreEqual(3, collection.Count);
    }

    [Test]
    public void InsertAt_ShouldThrowExceptionWhenInvalidIndex()
    {
        Collection<int> collection = new Collection<int>(1, 2, 3);

        int min = 0;
        int max = collection.Count;
        string expectedMessage = $"Parameter should be in the range [{min}...{max}]";

        int invalidIndex1 = -1;
        int invalidIndex2 = 5;

        var ex1 = Assert.Throws<ArgumentOutOfRangeException>(() => collection.InsertAt(invalidIndex1, 0));
        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => collection.InsertAt(invalidIndex2, 0));

        StringAssert.Contains(expectedMessage, ex1.Message);
        StringAssert.Contains(expectedMessage, ex2.Message);

        Assert.AreEqual("index", ex1.ParamName);
        Assert.AreEqual(invalidIndex1, ex1.ActualValue);

        Assert.AreEqual("index", ex2.ParamName);
        Assert.AreEqual(invalidIndex2, ex2.ActualValue);
    }

    [Test]
    public void Exchange_ShouldSwapItems()
    {
        Collection<string> collection = new Collection<string>("left", "right");
        collection.Exchange(0, 1);

        Assert.AreEqual("[right, left]", collection.ToString());
    }

    [Test]
    public void Exchange_ShouldThrowExceptionWhenInvalidIndex()
    {
        Collection<int> collection = new Collection<int>(1, 2);

        int min = 0;
        int max = collection.Count - 1;
        string expectedMessage = $"Parameter should be in the range [{min}...{max}]";

        int invalidIndex1 = -1;
        int invalidIndex2 = 2;

        var ex1 = Assert.Throws<ArgumentOutOfRangeException>(() => collection.Exchange(invalidIndex1, 1));
        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => collection.Exchange(0, invalidIndex2));

        StringAssert.Contains(expectedMessage, ex1.Message);
        StringAssert.Contains(expectedMessage, ex2.Message);

        Assert.AreEqual("index1", ex1.ParamName);
        Assert.AreEqual(invalidIndex1, ex1.ActualValue);

        Assert.AreEqual("index2", ex2.ParamName);
        Assert.AreEqual(invalidIndex2, ex2.ActualValue);
    }

    [Test]
    public void RemoveAt_ShouldRemoveItemAndShift()
    {
        Collection<int> collection = new Collection<int>(1, 2, 3);
        int removed = collection.RemoveAt(1);

        Assert.AreEqual(2, removed);
        Assert.AreEqual("[1, 3]", collection.ToString());
        Assert.AreEqual(2, collection.Count);
    }

    [Test]
    public void RemoveAt_ShouldThrowExceptionForInvalidIndex()
    {
        Collection<int> collection = new Collection<int>(1);
        
        int min = 0;
        int max = collection.Count - 1;
        string expectedMessage = $"Parameter should be in the range [{min}...{max}]";

        int invalidIndex1 = 1;
        int invalidIndex2 = -1;

        var ex1 = Assert.Throws<ArgumentOutOfRangeException>(() => collection.RemoveAt(invalidIndex1));
        var ex2 = Assert.Throws<ArgumentOutOfRangeException>(() => collection.RemoveAt(invalidIndex2));

        StringAssert.Contains(expectedMessage, ex1.Message);
        StringAssert.Contains(expectedMessage, ex2.Message);

        Assert.AreEqual("index", ex1.ParamName);
        Assert.AreEqual(invalidIndex1, ex1.ActualValue);

        Assert.AreEqual("index", ex2.ParamName);
        Assert.AreEqual(invalidIndex2, ex2.ActualValue);
    }

    [Test]
    public void Clear_ShouldRemoveAllItems()
    {
        Collection<int> collection = new Collection<int>(1, 2, 3);
        collection.Clear();

        Assert.AreEqual("[]", collection.ToString());
        Assert.AreEqual(0, collection.Count);
    }

    [Test]
    public void ToString_ShouldReturnCorrectRepresentation()
    {
        Collection<string> collection = new Collection<string>("A", "B");

        Assert.AreEqual("[A, B]", collection.ToString());
    }

    [Test]
    public void Collection_Performance_1MillionItems()
    {
        Collection<int> collection = new Collection<int>();

        for (int i = 0; i < 1_000_000; i++)
        {
            collection.Add(i);
        }

        Assert.AreEqual(1_000_000, collection.Count);
        Assert.That(collection.Capacity >= collection.Count);
        Assert.AreEqual(0, collection[0]);
        Assert.AreEqual(999_999, collection[999_999]);
    }
}
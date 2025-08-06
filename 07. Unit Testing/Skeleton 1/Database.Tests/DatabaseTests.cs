using System;

namespace Database.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        public void Constructor_ShouldInitializeDatabaseCorrectly()
        {
            int[] data = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
            
            var database = new Database(data);
            
            Assert.That(database.Count, Is.EqualTo(data.Length));
        }

        [Test]
        public void Constructor_ShouldThrowExceptionWhenDataIsBigger()
        {
            int[] data = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};

            var database = new Database();
            Assert.Throws<InvalidOperationException>(() => database = new Database(data), "Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void Add_ShouldAddElementCorrectly()
        {
            int[] data = { 1, 2, 3 };
            var database = new Database(data);
            
            database.Add(4);
            
            Assert.That(database.Count, Is.EqualTo(data.Length + 1));
        }
        
        [Test]
        public void Add_ShouldThrowExceptionWhenDatabaseIsBigger()
        {
            int[] data = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
            var database = new Database(data);
            
            
            Assert.Throws<InvalidOperationException>(() => database.Add(17), "Array's capacity must be exactly 16 integers!");
        }

        [Test]
        public void Remove_ShouldRemoveElementCorrectly()
        {
            int[] data = { 1, 2, 3 };
            var database = new Database(data);
            
            database.Remove();
            
            Assert.That(database.Count, Is.EqualTo(data.Length - 1));
        }

        [Test]
        public void Remove_ShouldThrowExceptionWhenEmpty()
        {
            int[] data = Array.Empty<int>();
            var database = new Database(data);
            
            Assert.Throws<InvalidOperationException>(() => database.Remove(), "The collection is empty!");
        }

        [Test]
        public void Fetch_ShouldReturnDatabaseCopyCorrectly()
        {
            int[] data = { 1, 2, 3 };
            var database = new Database(data);
            
            var copy = database.Fetch();
            
            Assert.That(copy.Length, Is.EqualTo(data.Length));

            for (var i = 0; i < copy.Length; i++)
            {
                Assert.That(copy[i], Is.EqualTo(data[i]));
            }
        }
    }
}

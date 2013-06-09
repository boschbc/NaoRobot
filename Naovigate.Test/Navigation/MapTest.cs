using System;
using System.IO;
using NUnit.Framework;
using Naovigate.Navigation;
namespace Naovigate.Test.Navigation
{
    
    [TestFixture, Timeout(1000)]
    public class MapTest : RequireMap
    {
        [Test]
        public void SetGetTest()
        {
            Tile expected = new Tile(2, 2, 654);
            Map.AddTile(expected);
            Assert.AreEqual(expected, Map.TileAt(2, 2));
        }

        [Test, ExpectedException(typeof(IndexOutOfRangeException))]
        public void OutOfRangeSetTest()
        {
            Map.AddTile(new Tile(3, 7, 4673));
        }

        [Test]
        public void ConsistencySetTest()
        {
            Map.AddTile(new Tile(1, 2, 42));
            Assert.AreEqual(1, Map.TileAt(1, 2).X);
            Assert.AreEqual(2, Map.TileAt(1, 2).Y);
        }

        [Test]
        public void HeightTest()
        {
            Assert.AreEqual(4, Map.Height);
        }

        [Test]
        public void WidthTest()
        {
            Assert.AreEqual(3, Map.Width);
        }

        [Test]
        public void WithinBordersInvalidTest()
        {
            Assert.IsFalse(Map.WithinBorders(3, 3));
        }

        [Test]
        public void WithinBordersValidTest()
        {
            Assert.IsTrue(Map.WithinBorders(2, 3));
        }
    }
}

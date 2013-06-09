using System;
using NUnit.Framework;
using Naovigate.Navigation;

namespace Naovigate.Test.Navigation
{
    public class TileTest : RequireMap
    {
        [Test]
        public void TileAtLeftTest()
        {
            Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Direction.Left));
        }

        [Test]
        public void TileAtRightTest()
        {
            Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Direction.Right));
        }

        [Test]
        public void TileAtUpTest()
        {
            Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Direction.Up));
        }

        [Test]
        public void TileAtDownTest()
        {
            Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Direction.Down));
        }

        [Test]
        public void SetWallAtTest()
        {
            Tile t = new Tile(0, 0);
            t.SetWallAt(Direction.Left, true);
            Assert.IsTrue(t.HasWallAt(Direction.Left));
        }

        [Test]
        public void SetMarkerAtTest()
        {
            Tile t = new Tile(0, 0);
            t.SetMarkerAt(Direction.Right, 123);
            Assert.IsTrue(t.HasMarkerAt(Direction.Right));
            Assert.AreEqual(123, t.MarkerAt(Direction.Right));
        }
    }
}

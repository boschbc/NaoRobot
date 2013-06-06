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
            Map.Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Left));
        }

        [Test]
        public void TileAtRightTest()
        {
            Map.Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Right));
        }

        [Test]
        public void TileAtUpTest()
        {
            Map.Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Up));
        }

        [Test]
        public void TileAtDownTest()
        {
            Map.Tile t = Map.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Down));
        }

        [Test]
        public void SetWallAtTest()
        {
            Map.Tile t = new Map.Tile(0, 0);
            t.SetWallAt(Map.Direction.Left, true);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Left));
        }

        [Test]
        public void SetMarkerAtTest()
        {
            Map.Tile t = new Map.Tile(0, 0);
            t.SetMarkerAt(Map.Direction.Right, 123);
            Assert.IsTrue(t.HasMarkerAt(Map.Direction.Right));
            Assert.AreEqual(123, t.MarkerAt(Map.Direction.Right));
        }
    }
}

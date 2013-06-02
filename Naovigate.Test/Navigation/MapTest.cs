using System;
using System.IO;
using NUnit.Framework;
using Naovigate.Navigation;
namespace Naovigate.Test.Navigation
{
    
    public class MapTest
    {
        private string[] wallMap = {
                                 /*
                                  _ _ _
                                 |_| | |
                                 | |_| |
                                 |_ _ _|
                                 |_ _ _|
                                
                                 */
                                "S 3 4",
                                "W 0 0 0",
                                "W 0 0 1",
                                "W 0 0 2",
                                "W 0 0 3",

                                "W 1 0 0",
                                "W 1 0 1",
                                "W 1 0 3",
                                
                                "W 2 0 0",
                                "W 2 0 1",
                                "W 2 0 3",

                                "W 0 1 0",
                                "W 0 1 1",
                                "W 0 1 3",

                                "W 1 1 1",
                                "W 1 1 2",
                                "W 1 1 3",

                                "W 2 1 1",
                                "W 2 1 3",

                                "W 0 2 2",
                                "W 0 2 3",

                                "W 1 2 0",
                                "W 1 2 2",

                                "W 2 2 1",
                                "W 2 2 2",
                                };
        private static string mapFile = "MapTest.map";
        private Map m;

        [TestFixtureSetUp]
        public void InitMaps()
        {

        }

        [SetUp]
        public void Setup()
        {
            File.AppendAllLines(mapFile, wallMap);
            m = Map.Parse(mapFile);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(mapFile);
        }

        [Test]
        public void TileAtLeftTest()
        {
            Map.Tile t = m.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Left));
        }

        [Test]
        public void TileAtRightTest()
        {
            Map.Tile t = m.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Right));
        }

        [Test]
        public void TileAtUpTest()
        {
            Map.Tile t = m.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Up));
        }

        [Test]
        public void TileAtDownTest()
        {
            Map.Tile t = m.TileAt(0, 0);
            Assert.IsTrue(t.HasWallAt(Map.Direction.Down));
        }
    }
}

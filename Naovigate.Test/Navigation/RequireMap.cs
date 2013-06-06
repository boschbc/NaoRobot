using System;
using System.IO;
using NUnit.Framework;
using Naovigate.Navigation;

namespace Naovigate.Test.Navigation
{
    public abstract class RequireMap
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

        protected Map Map
        {
            get;
            private set;
        }

        [SetUp]
        public void Setup()
        {
            File.AppendAllLines(mapFile, wallMap);
            Map = Map.Parse(mapFile);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(mapFile);
        }
    }
}

using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Naovigate.Navigation
{
    /// <summary>
    /// A class that represents the overview map of the world we're discovering.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Constants that are used in the map format to indicate the type of this line.
        /// </summary>
        public enum EntryType
        {
            Size = 'S',
            WallInfo = 'W',
            MarkerInfo = 'M'
        }

        /// <summary>
        /// A simple direction.
        /// </summary>
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        /// <summary>
        /// A single tile in the map. Contains adjoining wall and marker information.
        /// </summary>
        public class Tile
        {
            private int[] markers;
            private bool[] walls;
            private int x;
            private int y;

            public Tile(int x, int y)
            {
                this.x = x;
                this.y = y;
                this.markers = new[] { -1, -1, -1, -1 };
                this.walls = new[] { false, false, false, false };
            }

            /// <summary>
            /// Returns whether a wall is present or not in given direction.
            /// </summary>
            public bool HasWallAt(Direction d)
            {
                return this.walls[(int)d];
            }

            /// <summary>
            /// Set if a wall if present or not in given direction.
            /// </summary>
            public void SetWallAt(Direction d, bool wall)
            {
                this.walls[(int)d] = wall;
            }

            /// <summary>
            /// Returns whether a marker is present or not in given direction.
            /// </summary>
            public bool HasMarkerAt(Direction d)
            {
                return this.markers[(int)d] >= 0;
            }

            /// <summary>
            /// Return the marker ID in direction d;
            /// </summary>
            public int MarkerAt(Direction d)
            {
                return this.markers[(int)d];
            }

            /// <summary>
            /// Set if a marker is present or not in given direction.
            /// </summary>
            public void SetMarkerAt(Direction d, int marker)
            {
                this.markers[(int)d] = marker;
            }

            public int X
            {
                get { return this.x; }
            }

            public int Y
            {
                get { return this.y; }
            }
        }

        private int height;
        private int width;
        private Tile[,] tiles;

        /// <summary>
        /// Parse the map contained in file file.
        /// </summary>
        /// <param name="file">The file to parse from.</param>
        /// <throws>InvalidDataException</throws>
        /// <returns>A parsed map.</returns>
        public static Map Parse(string file)
        {
            int width = 0, height = 0, x, y, id;
            Direction direction;
            bool truth;

            // The parsed tiles.
            Tile[,] tiles = null;

            using (FileStream f = File.Open(file, FileMode.Open))
            using (StreamReader r = new StreamReader(f))
            {
                // Read command line.
                string[] line = r.ReadLine().Split(' ', '\t', '\n');

                // Determine command type.
                switch (line[0][0])
                {
                    // Metadata about the map size.
                    case (char)EntryType.Size:
                        ParseSize(line, out width, out height);

                        tiles = new Tile[width, height];
                        for (int i = 0; i < width; i++)
                            for (int j = 0; j < height; j++)
                                tiles[i, j] = new Tile(i, j);

                        break;

                    // Info about wall presence on a certain (x, y, direction).
                    case (char)EntryType.WallInfo:
                        ParsePositionFlag(line, out x, out y, out direction, out truth);
                        tiles[x, y].SetWallAt(direction, truth);

                        // Set adjoint point walls, too.
                        if (direction == Direction.Left && x > 0)
                            tiles[x - 1, y].SetWallAt(Direction.Right, truth);
                        else if (direction == Direction.Right && x < width - 1)
                            tiles[x + 1, y].SetWallAt(Direction.Left, truth);
                        else if (direction == Direction.Up && y > 0)
                            tiles[x, y - 1].SetWallAt(Direction.Down, truth);
                        else if (direction == Direction.Down && y < height - 1)
                            tiles[x, y + 1].SetWallAt(Direction.Up, truth);
                        break;

                    // Info about marker presence on a certain (x, y, direction).
                    case (char)EntryType.MarkerInfo:
                        ParsePositionValue(line, out x, out y, out direction, out id);
                        tiles[x, y].SetMarkerAt(direction, id);
                        break;
                }
            }

            Map m = new Map(tiles);
            return m;
        }

        /// <summary>
        /// Parse a size entry.
        /// </summary>
        private static void ParseSize(string[] line, out int width, out int height)
        {
            // Default values.
            width = 0;
            height = 0;
            
            // Validate line sanity.
            if (line.Length < 3)
                throw new InvalidDataException("Reached EOL before being able to read map size.");

            // Parse line entries.
            try {
                width = Int32.Parse(line[1]);
                height = Int32.Parse(line[2]);
            } catch (System.Exception e) {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid map size specified.");
            }
        }

        /// <summary>
        /// Parse a position flag entry, containing a position, direction and truth value. Used for marker and wall entries.
        /// </summary>
        private static void ParsePositionFlag(string[] line, out int x, out int y, out Direction dir, out bool truth)
        {
            // Default values.
            x = 0;
            y = 0;
            dir = Direction.Up;
            truth = false;

            // Validate line sanity.
            if (line.Length < 3)
                throw new InvalidDataException("Reached EOL before being able to read marker info.");

            // Parse line entries.
            try {
                x = Int32.Parse(line[1]);
                y = Int32.Parse(line[2]);
                dir = (Direction)Enum.ToObject(typeof(Direction), Byte.Parse(line[3]));
                truth = true;
            } catch (System.Exception e) {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid marker info entry specified.");
            }
        }

        /// <summary>
        /// Parse a position flag entry, containing a position, direction and truth value. Used for marker and wall entries.
        /// </summary>
        private static void ParsePositionValue(string[] line, out int x, out int y, out Direction dir, out int id)
        {
            // Default values.
            x = 0;
            y = 0;
            dir = Direction.Up;
            id = -1;

            // Validate line sanity.
            if (line.Length < 4)
                throw new InvalidDataException("Reached EOL before being able to read marker info.");

            // Parse line entries.
            try
            {
                x = Int32.Parse(line[1]);
                y = Int32.Parse(line[2]);
                dir = (Direction)Enum.ToObject(typeof(Direction), Byte.Parse(line[3]));
                id = Int32.Parse(line[4]);
            }
            catch (System.Exception e)
            {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid marker info entry specified.");
            }
        }

        private Map(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        /// <summary>
        /// The actual tiles.
        /// </summary>
        public Tile[,] Tiles
        {
            get { return this.tiles; }
            set { this.tiles = value; }
        }

        /// <summary>
        /// Retrieve the tile at position (x, y).
        /// </summary>
        public Tile TileAt(int x, int y)
        {
            return this.tiles[x, y];
        }

        /// <summary>
        /// Set the tile at position (x, y).
        /// </summary>
        public void SetTile(int x, int y, Tile t)
        {
            this.tiles[x, y] = t;
        }

        /// <summary>
        /// Returns whether or not a certain point is within borders.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool WithinBorders(int x, int y)
        {
            return x >= 0 && x < this.width && y > 0 && y < this.height;
        }
    }
}


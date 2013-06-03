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
            MarkerInfo = 'M',
            TileInfo = 'I'
        }

        /// <summary>
        /// A simple direction.
        /// </summary>
        public enum Direction
        {
            Left = 3,
            Right = 1,
            Up = 0,
            Down = 2
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
            private int id;

            public Tile(int x, int y, int id = -1)
            {
                this.x = x;
                this.y = y;
                this.id = id;
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
                d.ToAngle();
                this.markers[(int)d] = marker;
            }

            /// <summary>
            /// The X coordinate of this tile.
            /// </summary>
            public int X
            {
                get { return this.x; }
            }

            /// <summary>
            /// The Y coordinate of this tile.
            /// </summary>
            public int Y
            {
                get { return this.y; }
            }

            /// <summary>
            /// The tile ID.
            /// </summary>
            /// <value>The ID.</value>
            public int ID
            {
                get { return this.id; }
                set { this.id = value; }
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
                while (!r.EndOfStream)
                {
                    // Read command line.
                    string[] line = r.ReadLine().Split(' ', '\t', '\n');

                    // Determine command type.
                    switch (line[0][0])
                    {
                        // Metadata about the map size.
                        case (char)EntryType.Size:
                            ParseSize(line, out width, out height);

                            tiles = new Tile[height, width];
                            for (int i = 0; i < height; i++)
                                for (int j = 0; j < width; j++)
                                    tiles[i, j] = new Tile(j, i);

                            break;

                        // Info about wall presence on a certain (x, y, direction).
                        case (char)EntryType.WallInfo:
                            ParsePositionFlag(line, out x, out y, out direction, out truth);
                            tiles[y, x].SetWallAt(direction, truth);

                            // Set adjoint point walls, too.
                            if (direction == Direction.Left && x > 0)
                                tiles[y, x - 1].SetWallAt(Direction.Right, truth);
                            else if (direction == Direction.Right && x < width - 1)
                                tiles[y, x + 1].SetWallAt(Direction.Left, truth);
                            else if (direction == Direction.Up && y > 0)
                                tiles[y - 1, x].SetWallAt(Direction.Down, truth);
                            else if (direction == Direction.Down && y < height - 1)
                                tiles[y + 1, x].SetWallAt(Direction.Up, truth);
                            break;

                        // Info about marker presence on a certain (x, y, direction).
                        case (char)EntryType.MarkerInfo:
                            ParsePositionValue(line, out x, out y, out direction, out id);
                            tiles[y, x].SetMarkerAt(direction, id);
                            break;
                           
                        // Info about a tile.
                        case (char)EntryType.TileInfo:
                            ParsePositionValue(line, out x, out y, out id);
                            tiles[y, x].ID = id;
                            break;
                        
                        // What this?
                        default:
                            throw new InvalidDataException(String.Format("Could not parse map data: unknown data entry {0}", line[0][0]));
                    }
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
            if (line.Length < 4)
                throw new InvalidDataException("Reached EOL before being able to read marker info.");

            // Parse line entries.
            try {
                x = Int32.Parse(line[1]);
                y = Int32.Parse(line[2]);
                dir = (Direction)Enum.ToObject(typeof(Direction), Byte.Parse(line[3]));
                truth = true;
            } catch (Exception e) {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid marker info entry specified.");
                throw;
            }
        }

        /// <summary>
        /// Parse a position flag entry, containing a position, direction and truth value. Used for marker and wall entries.
        /// </summary>
        private static void ParsePositionValue(string[] line, out int x, out int y, out int id)
        {
            // Default values.
            x = 0;
            y = 0;
            id = -1;

            // Validate line sanity.
            if (line.Length < 4)
                throw new InvalidDataException("Reached EOL before being able to read marker info.");
            
            // Parse line entries.
            try
            {
                x = Int32.Parse(line[1]);
                y = Int32.Parse(line[2]);
                id = Int32.Parse(line[3]);
            }
            catch (Exception e)
            {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid marker info entry specified.");
                throw;
            }
        }

        /// <summary>
        /// Parse a position value entry, containing a position, direction and truth value. Used for marker and wall entries.
        /// </summary>
        private static void ParsePositionValue(string[] line, out int x, out int y, out Direction dir, out int id)
        {
            // Default values.
            x = 0;
            y = 0;
            dir = Direction.Up;
            id = -1;

            // Validate line sanity.
            if (line.Length < 5)
                throw new InvalidDataException("Reached EOL before being able to read marker info.");

            // Parse line entries.
            try
            {
                x = Int32.Parse(line[1]);
                y = Int32.Parse(line[2]);
                dir = (Direction)Enum.ToObject(typeof(Direction), Byte.Parse(line[3]));
                id = Int32.Parse(line[4]);
            }
            catch (Exception e)
            {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid marker info entry specified.");
                throw;
            }
        }

        private Map(Tile[,] tiles)
        {
            this.height = tiles.GetLength(0);
            this.width = tiles.GetLength(1);
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
        /// Gets the map width.
        /// </summary>
        public int Width
        {
            get { return this.width; }
        }

        /// <summary>
        /// Gets the map height.
        /// </summary>
        public int Height
        {
            get { return this.height; }
        }

        /// <summary>
        /// Retrieve the tile at position (x, y).
        /// </summary>
        public Tile TileAt(int x, int y)
        {
            return this.tiles[y, x];
        }

        /// <summary>
        /// Receive the tile with ID id.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Tile TileWithID(int id)
        {
            if (id == -1)
                return null;

            foreach (Tile t in this.tiles)
                if (t.ID == id)
                    return t;
            return null;
        }

        /// <summary>
        /// Set the tile at position (x, y).
        /// </summary>
        public void SetTile(int x, int y, Tile t)
        {
            this.tiles[y, x] = t;
        }

        /// <summary>
        /// Returns whether or not a certain point is within borders.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool WithinBorders(int x, int y)
        {
            return x >= 0 && x < this.width && y >= 0 && y < this.height;
        }

    }

    public static class DirectionExtension
    {
        /// <summary>
        /// Convert this direction to degrees in the range [0, 360)
        /// </summary>
        public static float ToAngle(this Map.Direction dir)
        {
            if (dir == Map.Direction.Up)
                return 0.0f;
            if (dir == Map.Direction.Right)
                return 90.0f;
            if (dir == Map.Direction.Down)
                return 180.0f;
            if (dir == Map.Direction.Left)
                return 270.0f;
            throw new ArgumentException("Direction is not a valid direction.");
        }

        /// <summary>
        /// Convert this direction to radians in the range [-pi, pi].
        /// </summary>
        public static double ToRadian(this Map.Direction dir)
        {
            double multiplier = 0.0;
            if (dir == Map.Direction.Up)
                multiplier = 0.0;
            else if (dir == Map.Direction.Right)
                multiplier = 0.5;
            else if (dir == Map.Direction.Down)
                multiplier = 1.0;
            else if (dir == Map.Direction.Left)
                multiplier = -0.5;
            return multiplier * (Math.PI / 180.0);
        }
    }

}


using System;
using System.IO;

namespace Naovigate.Navigation
{
    public static class MapParser
    {
        /// <summary>
        /// Constants that are used in the map format to indicate the type of this line.
        /// </summary>
        public enum EntryType
        {
            Size = 'S',
            WallInfo = 'W',
            MarkerInfo = 'M',
            TileInfo = 'I',
            Comment = '#',
        }


        public static int CurrentLineNr
        {
            get{ return lineNr; }
        }
        private static int lineNr;

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
            lineNr = 0;

            // The parsed tiles.
            Tile[,] tiles = null;

            using (FileStream f = File.Open(file, FileMode.Open))
            using (StreamReader r = new StreamReader(f))
            {
                while (!r.EndOfStream)
                {
                    // Read command line.
                    string[] line = r.ReadLine().Split(' ', '\t', '\n');
                    lineNr++;
                    // Determine command type.
                    switch (line[0][0])
                    {
                        // Metadata about the map size.
                        case (char)EntryType.Size:
                            ParseSize(line, out width, out height);
                            tiles = CreateTiles(width, height);
                            break;

                        // Info about wall presence on a certain (x, y, direction).
                        case (char)EntryType.WallInfo:
                            ParsePositionFlag(line, out x, out y, out direction, out truth);
                            tiles[y, x].SetWallAt(direction, truth);
                            SetAdjointWalls(direction, tiles, x, y, truth);
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

                        // Comment
                        case (char)EntryType.Comment:
                            break;

                        // What this?
                        default:
                            throw new InvalidDataException(String.Format("Could not parse map data: unknown data entry {0}", line[0][0]));
                    }
                }
            }
            lineNr = -1;
            return new Map(tiles);
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
            Expected(line, 3, "map");

            // Parse line entries.
            try
            {
                width = Int32.Parse(line[1]);
                height = Int32.Parse(line[2]);
            }
            catch (Exception e)
            {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid map size specified: line " + lineNr);
                throw;
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
            Expected(line, 4, "marker or wall");

            // Parse line entries.
            try
            {
                x = Int32.Parse(line[1]);
                y = Int32.Parse(line[2]);
                dir = (Direction)Enum.ToObject(typeof(Direction), Byte.Parse(line[3]));
                truth = true;
            }
            catch (Exception e)
            {
                if (e is OverflowException || e is FormatException)
                    throw new InvalidDataException("Invalid marker info entry specified: line " + lineNr);
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
            Expected(line, 4, "marker");

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
                    throw new InvalidDataException("Invalid marker info entry specified: line " + lineNr);
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
            Expected(line, 5, "marker");

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
                    throw new InvalidDataException("Invalid marker info entry specified: line " + lineNr);
                throw;
            }
        }

        /// <summary>
        /// initialize the Tiles for the created map.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static Tile[,] CreateTiles(int width, int height)
        {
            Tile[,] tiles = new Tile[height, width];
            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    tiles[i, j] = new Tile(j, i);
            return tiles;
        }

        /// <summary>
        /// Set the wall status of the tiles next to the current tile.
        /// </summary>
        /// <param name="direction">The direction the wall is in</param>
        /// <param name="tiles">The tiles</param>
        /// <param name="x">The current tile's x position</param>
        /// <param name="y">The current tile's y position</param>
        /// <param name="truth">A boolean indicating if there is a wall or not</param>
        private static void SetAdjointWalls(Direction direction, Tile[,] tiles, int x, int y, bool truth)
        {
            int height = tiles.GetLength(0);
            int width = tiles.GetLength(1);
            if (direction == Direction.Left && x > 0)
                tiles[y, x - 1].SetWallAt(Direction.Right, truth);
            else if (direction == Direction.Right && x < width - 1)
                tiles[y, x + 1].SetWallAt(Direction.Left, truth);
            else if (direction == Direction.Up && y > 0)
                tiles[y - 1, x].SetWallAt(Direction.Down, truth);
            else if (direction == Direction.Down && y < height - 1)
                tiles[y + 1, x].SetWallAt(Direction.Up, truth);
        }

        /// <summary>
        /// Throw an exception with a message if the input doesnt have the required length.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="minLengthExpected"></param>
        /// <param name="infoRequired"></param>
        private static void Expected(string[] line, int minLengthExpected, string infoRequired)
        {
            if (line.Length < minLengthExpected)
            {
                throw new InvalidDataException("Reached EOL before being able to read required " + infoRequired + " info.");
            }
        }
    }
}

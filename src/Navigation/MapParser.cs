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
            TileInfo = 'I'
        }

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
            try
            {
                width = Int32.Parse(line[1]);
                height = Int32.Parse(line[2]);
            }
            catch (System.Exception e)
            {
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
    }
}

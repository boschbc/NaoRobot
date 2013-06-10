

namespace Naovigate.Navigation
{
    /// <summary>
    /// A class that represents the overview map of the world we're exploring.
    /// </summary>
    public class Map
    {
        public Map(Tile[,] tiles)
        {
            this.Height = tiles.GetLength(0);
            this.Width = tiles.GetLength(1);
            this.Tiles = tiles;
        }

        /// <summary>
        /// The actual tiles.
        /// </summary>
        public Tile[,] Tiles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the map width.
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the map height.
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// Retrieve the tile at position (x, y).
        /// </summary>
        public Tile TileAt(int x, int y)
        {
            return this.Tiles[y, x];
        }

        /// <summary>
        /// Retrieve the tile with ID id.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public Tile TileWithID(int id)
        {
            if (id == -1)
                return null;

            foreach (Tile t in this.Tiles)
                if (t.ID == id)
                    return t;
            return null;
        }

        /// <summary>
        /// Add the tile at position (x, y).
        /// This will replace the old time, if there was any.
        /// </summary>
        public void AddTile(Tile t)
        {
            this.Tiles[t.Y, t.X] = t;
        }

        /// <summary>
        /// Returns whether or not a certain point is within borders.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public bool WithinBorders(int x, int y)
        {
            return x >= 0 && x < this.Width && y >= 0 && y < this.Height;
        }
    }
}


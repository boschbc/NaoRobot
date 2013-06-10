
namespace Naovigate.Navigation
{
    /// <summary>
    /// A single tile in the map. Contains adjoining wall and marker information.
    /// </summary>
    public class Tile
    {
        private int[] markers;
        private bool[] walls;

        public Tile(int x, int y, int id = -1)
        {
            this.X = x;
            this.Y = y;
            this.ID = id;
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
            return MarkerAt(d) != -1;
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

        /// <summary>
        /// The X coordinate of this tile.
        /// </summary>
        public int X
        {
            get;
            set;
        }

        /// <summary>
        /// The Y coordinate of this tile.
        /// </summary>
        public int Y
        {
            get;
            set;
        }

        /// <summary>
        /// The tile ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID
        {
            get;
            set;
        }

        public override string ToString()
        {
            return base.ToString()+"("+X+", "+Y+")";
        }
    }
}

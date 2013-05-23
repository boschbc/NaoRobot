using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Naovigate.Movement
{
    /// <summary>
    /// A class that represents the overview map of the world we're discovering.
    /// </summary>
    public class Map
    {
        public enum Tile
        {
            Free,
            Occupied,
            Target
        }

        private int height;
        private int width;
        private Tile[][] tiles;

        public Map Parse(int[][] tiles)
        {
            Map m = new Map(tiles.Length, tiles.Length > 0 ? tiles[0].Length : 0);
            for (int i = 0; i < tiles.Length; i++) {
                for (int j = 0; j < tiles.Length; j++) {
                    m.SetTile(i, j, (Tile)Enum.ToObject(typeof(Tile), tiles[i][j]));
                }
            }
            return m;
        }

        private Map(int width, int height)
        {
            this.BuildTiles(width, height, Tile.Free);
        }

        private void BuildTiles(int width, int height, Tile? initialValue = null)
        {
            this.width = width;
            this.height = height;
            this.tiles = new Tile[width][];
            for (int i = 0; i < width; i++)
            {
                this.tiles[i] = new Tile[height];
                if (initialValue.HasValue)
                    for (int j = 0; j < height; j++)
                        this.tiles[i][j] = initialValue.Value;
            }
        }

        public void SetTiles(Tile[][] tiles)
        {
            this.BuildTiles(tiles.Length, tiles.Length > 0 ? tiles[0].Length : 0);
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    this.tiles[i][j] = tiles[i][j];
        }

        public void SetTile(int width, int height, Tile type)
        {
            Debug.Assert(width >= 0 && height >= 0 && width < this.width && height < this.width, "Out of map bounds.");
            this.tiles[width][height] = type;
        }

        public Tile GetTile(int width, int height)
        {
            Debug.Assert(width >= 0 && height >= 0 && width < this.width && height < this.width, "Out of map bounds.");
            return this.tiles[width][height];
        }
    }
}


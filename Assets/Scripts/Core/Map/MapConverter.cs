using Core.Constants;
using Core.GridSystem;
using Managers.Log;
using UnityEngine;

namespace Core.Map
{
    public static class MapConverter
    {
        public static GridCell[,] ToGridArray(MapData mapData)
        {
            int width = GridConstants.GridWidth;
            int height = GridConstants.GridHeight;
            var grid = new GridCell[width, height];

            foreach (var cell in mapData.cells)
            {
                var x = cell.Position.x;
                var y = cell.Position.y;

                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    grid[x, y] = cell;
                }
                else
                {
                    GameLogger.Warn($"Cell position out of bounds: ({x}, {y})");
                }
            }

            return grid;
        }
    }

}
using Core.Math;
using Core.Tower;

namespace Core.GridSystem
{
    public class GridManager : IGridManager
    {
        public int Width => 15;
        public int Height => 10;
        private GridCell[,] _cells;
        
        public IGridCell GetCell(int x, int y)
        {
            return _cells[x, y];
        }

        public bool IsInsideBounds(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }

        public bool PlaceTower(Vec2Int position, TowerInstance tower)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveTower(Vec2Int position)
        {
            throw new System.NotImplementedException();
        }

        public bool CanBuildAt(Vec2Int position)
        {
            throw new System.NotImplementedException();
        }
    }
}


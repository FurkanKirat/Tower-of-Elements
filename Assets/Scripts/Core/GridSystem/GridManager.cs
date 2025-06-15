using Core.Constants;
using Core.Map;
using Core.Math;
using Core.Tower;

namespace Core.GridSystem
{
    public class GridManager : IGridManager
    {
        private readonly MapData _mapData;

        public GridManager(MapData mapData)
        {
            _mapData = mapData;
        }

        public IGridCell GetCell(int x, int y)
        {
            return _mapData.Grid[x, y];
        }

        public bool IsInsideBounds(int x, int y)
        {
            return x >= 0 && x < GridConstants.GridWidth && y >= 0 && y < GridConstants.GridHeight;
        }

        public bool PlaceTower(Vec2Int position, TowerInstance tower)
        {
            bool success = CanBuildAt(position);
            if(success)
                _mapData.Grid[position.x, position.y].TowerInstance = tower;
            return success;
        }

        public bool RemoveTower(Vec2Int position)
        {
            if (!IsInsideBounds(position.x, position.y)) return false;
            var cell = _mapData.Grid[position.x, position.y];
            if (cell.GridType == GridType.Buildable && cell.TowerInstance != null)
            {
                cell.TowerInstance = null;
                return true;
            }

            return false;
        }

        public bool CanBuildAt(Vec2Int position)
        {
            if (!IsInsideBounds(position.x, position.y)) return false;
            var cell = _mapData.Grid[position.x, position.y];
            return cell.GridType == GridType.Buildable && cell.TowerInstance == null;
        }
    }
}


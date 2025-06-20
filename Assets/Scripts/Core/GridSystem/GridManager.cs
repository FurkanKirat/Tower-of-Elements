using Core.Constants;
using Core.Map;
using Core.Math;
using UnityEngine;

namespace Core.GridSystem
{
    public class GridManager : IGridManager
    {
        private readonly MapData _mapData;

        public GridManager(MapData mapData)
        {
            _mapData = mapData;
        }

        public IGridCell GetCell(int x, int y) => _mapData.Grid[x, y];

        public IGridCell GetCell(Vector2Int pos) => GetCell(pos.x, pos.y);

        public IGridCell GetCell(Vec2Int pos) => GetCell(pos.x, pos.y);

        public bool IsInsideBounds(int x, int y)
        {
            return x >= 0 && x < GridConstants.GridWidth && y >= 0 && y < GridConstants.GridHeight;
        }

        public bool IsInsideBounds(Vector2Int gridPos) => 
            IsInsideBounds(gridPos.x, gridPos.y);

        public bool IsInsideBounds(Vec2Int gridPos) =>
            IsInsideBounds(gridPos.x, gridPos.y);

        public bool IsBuildable(int x, int y) =>
            _mapData.Grid[x, y]?.GridType == GridType.Buildable;
        

        public bool IsBuildable(Vector2Int pos) =>
            IsBuildable(pos.x, pos.y);

        public bool IsBuildable(Vec2Int pos) =>
            IsBuildable(pos.x, pos.y);
    }
}


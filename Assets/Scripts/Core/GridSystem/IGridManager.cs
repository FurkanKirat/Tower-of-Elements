using Core.Math;
using UnityEngine;

namespace Core.GridSystem
{
    public interface IGridManager
    {
        IGridCell GetCell(int x, int y);
        IGridCell GetCell(Vector2Int pos);
        IGridCell GetCell(Vec2Int pos);
        bool IsInsideBounds(int x, int y);
        bool IsInsideBounds(Vector2Int gridPos);
        bool IsInsideBounds(Vec2Int gridPos);
        
        bool IsBuildable(int x, int y);
        bool IsBuildable(Vector2Int pos);
        bool IsBuildable(Vec2Int pos);
    }
}
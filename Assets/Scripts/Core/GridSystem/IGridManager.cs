using Core.Math;
using Core.Tower;

namespace Core.GridSystem
{
    public interface IGridManager
    {
        IGridCell GetCell(int x, int y);
        bool IsInsideBounds(int x, int y);
        
        bool PlaceTower(Vec2Int position, TowerInstance tower);
        bool RemoveTower(Vec2Int position);
        bool CanBuildAt(Vec2Int position);

    }
}
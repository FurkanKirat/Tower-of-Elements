using Core.GridSystem;
using Core.Math;

namespace Editor.MapEditor.Tools
{
    public interface ITool
    {
        string Name { get; }
        bool IsActive { get; }
    
        void Activate();
        void Deactivate();

        void OnClick(Vec2Int gridPos, GridCell[,] grid);
        void DrawOverlay();
    }

}
using Core.Commands;
using Core.GridSystem;
using Core.Math;
using Editor.MapEditor.Commands;
using Managers.Log;

namespace Editor.MapEditor.Tools
{
    public class BrushTool : ITool
    {
        public string Name => "Brush";
        public bool IsActive { get; private set; }
        public int SelectedLayerIndex { get; set; } = 0;
        public string SelectedSpriteId { get; set; } = string.Empty;
        public Direction SelectedDirection { get; set; } = Direction.Up;
        
        private readonly CommandManager _commandManager;

        public BrushTool(CommandManager commandManager)
        {
            _commandManager = commandManager;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void OnClick(Vec2Int pos, GridCell[,] grid)
        {
            if (!IsInsideBounds(grid, pos)) return;

            var cell = grid[pos.x, pos.y];
            if (cell == null) return;

            var command = new SetSpriteLayerCommand(cell, SelectedLayerIndex, SelectedSpriteId, SelectedDirection);
            _commandManager.ExecuteCommand(command);
            GameLogger.Log("Click");
        }

        public void DrawOverlay()
        {
            // Optional: hover sprite, outline, etc.
        }

        private bool IsInsideBounds(GridCell[,] grid, Vec2Int pos)
        {
            GameLogger.Log("Checking bounds");
            return pos.x >= 0 && pos.x < grid.GetLength(0) && pos.y >= 0 && pos.y < grid.GetLength(1);
        }
    }

}
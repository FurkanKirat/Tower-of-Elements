using System;
using System.Collections.Generic;
using Core.GridSystem;
using Core.Math;

namespace Editor.MapEditor.Tools
{
    public class ToolManager
    {
        private readonly Dictionary<Type, ITool> _tools = new();
        private ITool _currentTool;

        public void RegisterTool(ITool tool)
        {
            var type = tool.GetType();
            _tools.TryAdd(type, tool);
        }

        public void SetTool<T>() where T : ITool
        {
            if (_tools.TryGetValue(typeof(T), out var tool))
            {
                _currentTool?.Deactivate();
                _currentTool = tool;
                _currentTool.Activate();
            }
        }

        public void ResetCurrentTool()
        {
            _currentTool?.Deactivate();
            _currentTool = null;
        }

        public void HandleClick(Vec2Int pos, GridCell[,] grid)
        {
            _currentTool?.OnClick(pos, grid);
        }

        public void Draw()
        {
            _currentTool?.DrawOverlay();
        }

        public ITool Current => _currentTool;

        public IEnumerable<ITool> AllTools => _tools.Values;
    }
}
using System;
using System.Collections.Generic;
using Editor.MapEditor.Views;

namespace Editor.MapEditor.Tools
{
    public class ToolPanelView
    {
        private readonly Dictionary<Type, IToolView> _toolViews;

        public ToolPanelView(params ITool[] tools)
        {
            _toolViews = new();

            foreach (var tool in tools)
            {
                if (tool is BrushTool brush)
                    _toolViews[typeof(BrushTool)] = new BrushToolView(brush);
            }
        }

        public void Draw(ITool currentTool)
        {
            if (_toolViews.TryGetValue(currentTool.GetType(), out var view))
            {
                view.Draw();
            }
        }

    }


}
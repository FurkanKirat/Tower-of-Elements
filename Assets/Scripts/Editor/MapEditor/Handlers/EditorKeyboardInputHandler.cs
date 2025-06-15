using Core.Commands;
using Editor.MapEditor.Tools;
using UnityEngine;

namespace Editor.MapEditor.Handlers
{
    public class EditorKeyboardInputHandler
    {
        private readonly CommandManager _commandManager;
        private readonly ToolManager _toolManager;
        
        public EditorKeyboardInputHandler(CommandManager commandManager, ToolManager toolManager)
        {
            _commandManager = commandManager;
            _toolManager = toolManager;
        }

        public void HandleInput()
        {
            var e = Event.current;

            if (e?.type != EventType.KeyDown)
                return;
            
            if ((e.control || e.command)  && e.keyCode == KeyCode.Z)
            {
                _commandManager.Undo();
                e.Use();
            }
            else if ((e.control || e.command)  && e.keyCode == KeyCode.Y)
            {
                _commandManager.Redo();
                e.Use();
            }
            else if (e.keyCode == KeyCode.B)
            {
                if (_toolManager.Current is BrushTool)
                {
                    _toolManager.ResetCurrentTool();
                }
                else 
                    _toolManager.SetTool<BrushTool>();
                e.Use();
            }
            
        }
    }

}


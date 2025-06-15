using Core.GridSystem;
using Core.Math;
using Editor.Helpers;
using Editor.MapEditor.Handlers;
using Editor.MapEditor.Tools;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor.Views
{
    public class GridView
    {
        private readonly GridCell[,] _grid;
        private readonly float _zoom;
        private readonly Vector2 _panOffset;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        private readonly ToolManager _toolManager;
        private readonly EditorMouseDragHandler _mouseDragHandler;
        
        public Rect Bounds => _gridRect;

        public Vec2Int? SelectedCellPos { get; private set; }
        

        public GridView(GridCell[,] grid, float zoom, Vector2 panOffset, 
            int tileWidth, int tileHeight, ToolManager toolManager, 
            EditorMouseDragHandler mouseDragHandler)
        {
            _grid = grid;
            _zoom = zoom;
            _panOffset = panOffset;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _toolManager = toolManager;
            _mouseDragHandler = mouseDragHandler;
        }

        private Vector2 _scroll;
        private Rect _gridRect;
        
        private Vec2Int? _hoveredCell = null;
        private float _hoverStartTime = 0f;
        private readonly float _tooltipDelay = 1.0f; 

        public void Draw()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            float contentWidth = _grid.GetLength(0) * _tileWidth * _zoom + 500;
            float contentHeight = _grid.GetLength(1) * _tileHeight * _zoom + 500;

            _gridRect = GUILayoutUtility.GetRect(contentWidth, contentHeight);
            Vector2 offset = _gridRect.position + _panOffset;

            Handles.BeginGUI();

            Vec2Int? currentCell = null;

            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    var cell = _grid[x, y];
                    Vector2 pos = new Vector2(x * _tileWidth, y * _tileHeight) * _zoom + offset;
                    Rect tileRect = new Rect(pos.x, pos.y, _tileWidth * _zoom, _tileHeight * _zoom);

                    SpriteDrawUtil.DrawCellLayers(cell, tileRect);
                    SpriteDrawUtil.DrawCellOutline(cell, tileRect, SelectedCellPos, x, y);

                    if (tileRect.Contains(Event.current.mousePosition))
                    {
                        currentCell = new Vec2Int(x, y);

                        // Hover tracking
                        if (_hoveredCell == null || _hoveredCell.Value != currentCell.Value)
                        {
                            _hoveredCell = currentCell.Value;
                            _hoverStartTime = Time.realtimeSinceStartup;
                        }
                    }
                }
            }

            // Drag handling (sadece gridde mouse varsa)
            if (currentCell != null)
            {
                foreach (var pos in _mouseDragHandler.Update(Event.current, currentCell))
                {
                    _toolManager.HandleClick(pos, _grid);
                    SelectedCellPos = pos;
                }
            }

            // Tooltip (hoveredCell varsa)
            if (_hoveredCell != null && Time.realtimeSinceStartup - _hoverStartTime > _tooltipDelay)
            {
                var cell = _grid[_hoveredCell.Value.x, _hoveredCell.Value.y];
                string tooltip = $"Pos: {_hoveredCell.Value.x}, {_hoveredCell.Value.y}\nGridType: {cell.GridType}";

                Vector2 mousePos = Event.current.mousePosition;
                Vector2 size = GUI.skin.box.CalcSize(new GUIContent(tooltip));
                Rect rect = new Rect(mousePos.x + 10, mousePos.y + 10, size.x + 10, size.y + 10);

                GUI.Box(rect, tooltip);
            }

            Handles.EndGUI();
            EditorGUILayout.EndScrollView();
        }


    }

}
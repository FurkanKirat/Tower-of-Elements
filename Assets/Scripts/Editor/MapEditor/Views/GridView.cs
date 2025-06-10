using Core.GridSystem;
using Core.Math;
using Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor.Views
{
    public class GridView
    {
        private GridCell[,] _grid;
        private float _zoom;
        private Vector2 _panOffset;
        private int _tileWidth;
        private int _tileHeight;
        
        public Rect Bounds => _gridRect;

        public Vec2Int? SelectedCellPos { get; private set; }

        public GridView(GridCell[,] grid, float zoom, Vector2 panOffset, int tileWidth, int tileHeight, bool isReadOnly)
        {
            _grid = grid;
            _zoom = zoom;
            _panOffset = panOffset;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        private Vector2 _scroll;
        private Rect _gridRect;

        public void Draw(string selectedSpriteId, int selectedLayerIndex, Direction selectedDirection, bool isReadOnly)
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            float contentWidth = _grid.GetLength(0) * _tileWidth * _zoom + 500;
            float contentHeight = _grid.GetLength(1) * _tileHeight * _zoom + 500;

            _gridRect = GUILayoutUtility.GetRect(contentWidth, contentHeight);
            Vector2 offset = _gridRect.position + _panOffset;

            Handles.BeginGUI();

            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    var cell = _grid[x, y];
                    Vector2 pos = new Vector2(x * _tileWidth, y * _tileHeight) * _zoom + offset;
                    Rect tileRect = new Rect(pos.x, pos.y, _tileWidth * _zoom, _tileHeight * _zoom);

                    SpriteDrawUtil.DrawCellLayers(cell, tileRect);
                    SpriteDrawUtil.DrawCellOutline(cell, tileRect, SelectedCellPos, x, y);
                    
                    if (Event.current.type == EventType.MouseDown && tileRect.Contains(Event.current.mousePosition) && Event.current.button == 0)
                    {
                        SelectedCellPos = new Vec2Int(x, y);
                        if (isReadOnly) continue;
                        var newLayer = new SpriteLayer
                        {
                            SpriteId = selectedSpriteId, 
                            Direction = selectedDirection
                        };
                        cell.SetLayer(selectedLayerIndex, newLayer);
                        cell.RecalculateGridType();
                        Event.current.Use();
                        GUI.changed = true;
                    }
                }
            }

            Handles.EndGUI();

            EditorGUILayout.EndScrollView();
        }

    }

}
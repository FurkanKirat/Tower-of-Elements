using Core.GridSystem;
using Core.Math;
using UnityEditor;
using UnityEngine;

namespace Editor.Helpers
{
    public static class SpriteDrawUtil
    {
        public static void DrawCellLayers(GridCell cell, Rect tileRect)
        {
            foreach (var layer in cell.SpriteLayers)
            {
                if(layer?.SpriteId == null) continue;
                var sprite = TileSpriteDatabase.Get(layer.SpriteId);
                if (sprite == null) continue;

                Matrix4x4 oldMatrix = GUI.matrix;
                
                Vector2 pivot = tileRect.center;
                float angle = layer.Direction.ToDegrees();

                GUIUtility.RotateAroundPivot(angle, pivot);
                GUI.DrawTexture(tileRect, sprite.texture);
                GUI.matrix = oldMatrix;
            }
        }
        
        public static void DrawCellOutline(GridCell cell, Rect rect, Vec2Int? selected, int x, int y)
        {
            bool isSelected = selected.HasValue && selected.Value.x == x && selected.Value.y == y;
            Color outlineColor = isSelected ? Color.yellow : Color.black;
            Handles.DrawSolidRectangleWithOutline(rect, new Color(1, 1, 1, 0.05f), outlineColor);
        }
    }

}
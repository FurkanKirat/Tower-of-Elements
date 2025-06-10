using Core.Database;
using Core.GridSystem;
using Core.Math;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Editor.Helpers
{
    public static class SpriteDrawUtil
    {
        public static void DrawSpriteWithUV(Sprite sprite, Rect drawRect)
        {
            if (sprite == null) return;
    
            Rect texCoords = new Rect(
                sprite.rect.x / sprite.texture.width,
                sprite.rect.y / sprite.texture.height,
                sprite.rect.width / sprite.texture.width,
                sprite.rect.height / sprite.texture.height
            );
    
            GUI.DrawTextureWithTexCoords(drawRect, sprite.texture, texCoords);
        }
    
        public static void DrawCellLayers(GridCell cell, Rect tileRect)
        {
            foreach (var layer in cell.SpriteLayers)
            {
                if(layer.SpriteId == null) continue;
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
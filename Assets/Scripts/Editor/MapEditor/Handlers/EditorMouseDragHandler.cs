using System.Collections.Generic;
using Core.Math;
using UnityEngine;

namespace Editor.MapEditor.Handlers
{
    public class EditorMouseDragHandler
    {
        private Vec2Int? _lastDraggedCell = null;
        public bool IsDragging { get; private set; }

        public IEnumerable<Vec2Int> Update(Event e, Vec2Int? currentCell)
        {
            var affected = new List<Vec2Int>();

            if (e.button != 0 || currentCell == null)
                return affected;

            if (e.type == EventType.MouseDown)
            {
                _lastDraggedCell = currentCell;
                IsDragging = true;
                affected.Add(currentCell.Value);
            }
            else if (e.type == EventType.MouseDrag && IsDragging)
            {
                if (_lastDraggedCell.HasValue && _lastDraggedCell.Value != currentCell.Value)
                {
                    foreach (var pos in GetLine(_lastDraggedCell.Value, currentCell.Value))
                        affected.Add(pos);

                    _lastDraggedCell = currentCell;
                }
            }
            else if (e.type == EventType.MouseUp)
            {
                IsDragging = false;
                _lastDraggedCell = null;
            }

            return affected;
        }

        private IEnumerable<Vec2Int> GetLine(Vec2Int start, Vec2Int end)
        {
            int dx = Mathf.Abs(end.x - start.x);
            int dy = Mathf.Abs(end.y - start.y);
            int sx = start.x < end.x ? 1 : -1;
            int sy = start.y < end.y ? 1 : -1;
            int err = dx - dy;

            int x = start.x;
            int y = start.y;

            while (true)
            {
                yield return new Vec2Int(x, y);
                if (x == end.x && y == end.y) break;

                int e2 = 2 * err;
                if (e2 > -dy) { err -= dy; x += sx; }
                if (e2 < dx) { err += dx; y += sy; }
            }
        }
    }
}
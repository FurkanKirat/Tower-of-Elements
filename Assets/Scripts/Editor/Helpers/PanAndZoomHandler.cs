namespace Editor.Helpers
{
    using UnityEngine;
    
    public class PanAndZoomHandler
    {
        public float Zoom { get; private set; } = 1f;
        public Vector2 PanOffset { get; private set; } = Vector2.zero;

        private Vector2 _dragStart;
        private bool _isDragging;

        public void HandleEvents(Rect interactionArea)
        {
            Event e = Event.current;
            Vector2 mousePos = e.mousePosition;

            if (!interactionArea.Contains(mousePos))
                return;

            if (e.type == EventType.ScrollWheel)
            {
                float zoomDelta = -e.delta.y * 0.1f;
                float newZoom = Mathf.Clamp(Zoom + zoomDelta, 0.25f, 3f);

                Vector2 localMouse = (mousePos - PanOffset) / Zoom;
                Zoom = newZoom;
                Vector2 newLocal = (mousePos - PanOffset) / Zoom;
                PanOffset += (newLocal - localMouse) * Zoom;

                e.Use();
            }
            else if (e.type == EventType.MouseDown && e.button == 2)
            {
                _isDragging = true;
                _dragStart = mousePos;
                e.Use();
            }
            else if (e.type == EventType.MouseDrag && _isDragging)
            {
                Vector2 delta = mousePos - _dragStart;
                PanOffset += delta;
                _dragStart = mousePos;
                e.Use();
            }
            else if (e.type == EventType.MouseUp && e.button == 2)
            {
                _isDragging = false;
                e.Use();
            }
        }
    }

}
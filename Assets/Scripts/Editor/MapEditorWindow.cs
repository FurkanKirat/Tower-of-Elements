using Core.GridSystem;
using Core.Math;
using UnityEditor;
using UnityEngine;
using Core.Database;
using Managers;

namespace Editor
{
    public class MapEditorWindow : EditorWindow
    {
        private const int GridWidth = 20;
        private const int GridHeight = 10;
        private const int TileWidth = 32;
        private const int TileHeight = 32;

        private readonly GridCell[,] _grid = new GridCell[GridWidth, GridHeight];
        private string _selectedSpriteId = "";
        private int _selectedLayerIndex = 0;
        private Direction _selectedDirection = Direction.Down;
        private Vector2 _paletteScroll;
        private Vec2Int? _selectedCellPos = null;

        // Zoom and pan variables
        private float _zoom = 1.0f;
        private Vector2 _panOffset = Vector2.zero;
        private Vector2 _dragStart;
        private bool _isDragging = false;

        private string _mapName;

        [MenuItem("Tools/Isometric Map Editor")]
        public static void ShowWindow() => GetWindow<MapEditorWindow>("Isometric Map Editor");

        private void OnEnable()
        {
            InitGrid();
            TileSpriteDatabase.Load();
        }

        private void OnGUI()
        {
            HandleZoom();
            HandlePan();

            GUILayout.Label("Isometric Map Editor", EditorStyles.boldLabel);
            _mapName = GUILayout.TextField("Map Name", EditorStyles.boldLabel);
            DrawSettingsPanel();
            DrawSpritePalette();
            DrawGrid();
        }

        private void InitGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            for (int y = 0; y < GridHeight; y++)
                _grid[x, y] = new GridCell {
                    Position = new Vec2Int(x, y),
                    GridType = GridType.Empty
                };
        }

        private void HandleZoom()
        {
            if (Event.current.type == EventType.ScrollWheel)
            {
                Vector2 mousePos = Event.current.mousePosition;
                float zoomDelta = -Event.current.delta.y * 0.1f;
                float newZoom = Mathf.Clamp(_zoom + zoomDelta, 0.25f, 3f);

                // World position of mouse before zoom
                Vector2 mouseWorldBefore = (mousePos - _panOffset) / _zoom;

                _zoom = newZoom;

                // World position of mouse after zoom
                Vector2 mouseWorldAfter = (mousePos - _panOffset) / _zoom;

                // Adjust pan so that the world position under the cursor stays fixed
                _panOffset += (mouseWorldAfter - mouseWorldBefore) * _zoom;

                Event.current.Use();
                Repaint();
            }
        }


        private void HandlePan()
        {
            if (Event.current.type == EventType.MouseDown && Event.current.button == 2)
            {
                _isDragging = true;
                _dragStart = Event.current.mousePosition;
                Event.current.Use();
            }
            else if (Event.current.type == EventType.MouseDrag && _isDragging)
            {
                Vector2 delta = Event.current.mousePosition - _dragStart;
                _panOffset += delta;
                _dragStart = Event.current.mousePosition;
                Event.current.Use();
                Repaint();
            }
            else if (Event.current.type == EventType.MouseUp && Event.current.button == 2)
            {
                _isDragging = false;
                Event.current.Use();
            }
        }

        private void DrawSettingsPanel()
        {
            GUILayout.Space(5);
            _selectedLayerIndex = EditorGUILayout.IntField("Selected Layer Index", _selectedLayerIndex);
            _selectedDirection = (Direction)EditorGUILayout.EnumPopup("Direction", _selectedDirection);
            GUILayout.Label("Selected Sprite ID: " + _selectedSpriteId);

            if (_selectedCellPos.HasValue)
                GUILayout.Label($"Selected Cell: ({_selectedCellPos.Value.x}, {_selectedCellPos.Value.y})");
        }

        private void DrawSpritePalette()
        {
            GUILayout.Label("Sprite Palette", EditorStyles.boldLabel);
            _paletteScroll = GUILayout.BeginScrollView(_paletteScroll, GUILayout.Height(100));
            GUILayout.BeginHorizontal();

            foreach (var spriteId in TileSpriteDatabase.GetAll.Keys)
            {
                var sprite = TileSpriteDatabase.Get(spriteId);
                if (sprite == null) continue;

                if (GUILayout.Button(new GUIContent(sprite.texture, spriteId), GUILayout.Width(64), GUILayout.Height(64)))
                {
                    _selectedSpriteId = spriteId;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndScrollView();
        }

        private void DrawGrid()
        {
            Vector2 offset = _panOffset + new Vector2(position.width / 2, position.height / 2);

            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    var cell = _grid[x, y];
                    Vector2 pos = new Vector2(x * TileWidth, y * TileHeight) * _zoom + offset;
                    Rect tileRect = new Rect(pos.x, pos.y, TileWidth * _zoom, TileHeight * _zoom);

                    DrawCellLayers(cell, tileRect);
                    DrawCellOutline(x, y, tileRect);
                    HandleCellClick(x, y, tileRect);
                }
            }
        }

        private void DrawCellLayers(GridCell cell, Rect tileRect)
        {
            foreach (var layer in cell.SpriteLayers)
            {
                if (string.IsNullOrEmpty(layer.SpriteId)) continue;

                string fullId = layer.SpriteId + $"_{layer.Direction.ToString().ToLower()}";
                var sprite = TileSpriteDatabase.Get(fullId) ?? TileSpriteDatabase.Get(layer.SpriteId);

                if (sprite != null)
                {
                    DrawSpriteWithUV(sprite, tileRect);
                }
                else
                {
                    GameLogger.Warn($"Missing sprite: {fullId}");
                }
            }
        }

        private void DrawSpriteWithUV(Sprite sprite, Rect drawRect)
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

        private void DrawCellOutline(int x, int y, Rect rect)
        {
            bool isSelected = _selectedCellPos.HasValue &&
                              _selectedCellPos.Value.x == x &&
                              _selectedCellPos.Value.y == y;

            Color outlineColor = isSelected ? Color.yellow : Color.black;
            Handles.DrawSolidRectangleWithOutline(rect, new Color(1, 1, 1, 0.05f), outlineColor);
        }

        private void HandleCellClick(int x, int y, Rect rect)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition) && Event.current.button == 0)
            {
                var cell = _grid[x, y];
                var newLayer = new SpriteLayer
                {
                    SpriteId = _selectedSpriteId,
                    Direction = _selectedDirection
                };

                cell.SetLayer(_selectedLayerIndex, newLayer);
                _selectedCellPos = new Vec2Int(x, y);

                Event.current.Use();
                Repaint();
            }
        }
    }
}

using System.Collections.Generic;
using Core.Constants;
using Core.GridSystem;
using Core.Math;
using UnityEditor;
using UnityEngine;
using Core.Database;
using Core.Entities;
using Core.Map;
using Editor.Helpers;
using Editor.MapEditor.Views;
using Managers;

namespace Editor.MapEditor.Windows
{
    public class MapEditorWindow : EditorWindow
    {
        private MapData _mapData;
        private GridCell[,] _grid;

        private PanAndZoomHandler _panAndZoom;
        private SpritePaletteView _paletteView;
        private SettingsPanelView _settingsView;
        private GridView _gridView;
        private MapSelectionPanelView _mapSelectionPanel;

        private const string MapsDirectory = PathNames.RootFolders.Maps;
        private bool _isReadOnlyMode = false;
        [MenuItem("Tools/Isometric Map Editor")]
        public static void ShowWindow() => GetWindow<MapEditorWindow>("Isometric Map Editor");

        private void OnEnable()
        {
            _mapData = new MapData
            {
                mapId = "",
                cells = new List<GridCell>(),
                enemySpawn = new Vec2Int(0, 0),
                enemies = new List<EnemyData>(),
            };
            _grid = new GridCell[GridConstants.GridWidth, GridConstants.GridHeight];
            InitGrid();
            TileSpriteDatabase.Load();

            _panAndZoom = new PanAndZoomHandler();
            _paletteView = new SpritePaletteView();
            _settingsView = new SettingsPanelView();
            _settingsView.SetMapData(_mapData);
            _gridView = new GridView(
                _grid,
                _panAndZoom.Zoom,
                _panAndZoom.PanOffset,
                GridConstants.TileWidth, 
                GridConstants.TileHeight,
                _isReadOnlyMode
            );
            _mapSelectionPanel = new MapSelectionPanelView(CreateNewMap, LoadFromJson);
        }

        private void OnGUI()
        {
            var directory = PathHelper.CombineWithResources(MapsDirectory);
            var maps = FileManager.GetFilesInDirectory(directory,".json");
            _mapSelectionPanel.Draw(maps);
            _panAndZoom.HandleEvents(_gridView.Bounds);
            _isReadOnlyMode = GUILayout.Toggle(_isReadOnlyMode, "Read-Only Mode", "Button", GUILayout.Width(150));

            GUILayout.Label("Isometric Map Editor", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Map Name", EditorStyles.boldLabel, GUILayout.Width(100));
            _mapData.mapId = GUILayout.TextField(_mapData.mapId, GUILayout.Width(300));
            GUILayout.EndHorizontal();

            _settingsView.SelectedSpriteId = _paletteView.DrawAndSelectSprite();
            _settingsView.Draw();

            _gridView.Draw(
                _settingsView.SelectedSpriteId,
                _settingsView.SelectedLayerIndex,
                _settingsView.SelectedDirection,
                _isReadOnlyMode
            );
            _settingsView.SelectedCellPos = _gridView.SelectedCellPos;
            
            if (GUILayout.Button("Save Map"))
                SaveToJson();
        }

        private void InitGrid()
        {
            for (int x = 0; x < GridConstants.GridWidth; x++)
                for (int y = 0; y < GridConstants.GridHeight; y++)
                    _grid[x, y] = new GridCell
                    {
                        Position = new Vec2Int(x, y),
                        GridType = GridType.Empty
                    };
        }

        private void SaveToJson()
        {
            _mapData.cells.Clear();
            foreach (var cell in _grid)
                _mapData.cells.Add(cell);

            string fileName = _mapData.mapId + PathNames.Extensions.Json;

            string fullDirectoryPath = PathHelper.CombineWithResources(MapsDirectory);
            string location = PathHelper.Combine(fullDirectoryPath, fileName);
            JsonHelper.Save(location, _mapData);
        }

        private void CreateNewMap()
        {
            _mapData = new MapData
            {
                mapId = "New Map",
                cells = new List<GridCell>(),
                enemySpawn = new Vec2Int(0, 0),
                enemies = new List<EnemyData>()
            };
            _grid = new GridCell[GridConstants.GridWidth, GridConstants.GridHeight];
            _gridView = new GridView(
                _grid,
                _panAndZoom.Zoom,
                _panAndZoom.PanOffset,
                GridConstants.TileWidth, 
                GridConstants.TileHeight,
                _isReadOnlyMode
            );
            _settingsView.SetMapData(_mapData);
            InitGrid();
            Repaint();
        }
        private void LoadFromJson(string mapId)
        {
            _mapData = JsonHelper.Load<MapData>(mapId);
            foreach (var cell in _mapData.cells)
            {
                _grid[cell.Position.x, cell.Position.y] = cell;
            }
            _settingsView.SetMapData(_mapData);
            Repaint();
        }
    }
}

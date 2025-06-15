using System.Collections.Generic;
using System.IO;
using Core.Commands;
using Core.Constants;
using Core.GridSystem;
using Core.Math;
using UnityEditor;
using UnityEngine;
using Core.Entities;
using Core.Map;
using Editor.Helpers;
using Editor.MapEditor.Handlers;
using Editor.MapEditor.Tools;
using Editor.MapEditor.Views;
using Managers;

namespace Editor.MapEditor.Windows
{
    public class MapEditorWindow : EditorWindow
    {
        private MapData _mapData;

        private PanAndZoomHandler _panAndZoom;
        private SettingsPanelView _settingsView;
        private GridView _gridView;
        private MapSelectionPanelView _mapSelectionPanel;
        private CommandManager _commandManager;
        private EditorKeyboardInputHandler _keyboardInputHandler;
        private EditorMouseDragHandler _mouseDragHandler;
        private ToolPanelView _toolPanel;
        private ToolManager _toolManager;
        
        private double _lastDrawTime;
        private const double FrameTime = 1.0 / 60.0;
        
        private string _mapsFullDirectory;
        [MenuItem("Tools/Map Editor")]
        public static void ShowWindow() => GetWindow<MapEditorWindow>("Map Editor");

        private void OnEnable()
        {
            _mapsFullDirectory = PathHelper.CombineWithResources(PathNames.RootFolders.Maps);
            TileSpriteDatabase.Load();
            _commandManager = new CommandManager();
            _panAndZoom = new PanAndZoomHandler();
            _settingsView = new SettingsPanelView(_commandManager);
            _mapSelectionPanel = new MapSelectionPanelView(CreateNewMap, LoadFromJson);

            _toolManager = new ToolManager();
            
            var brushTool = new BrushTool(_commandManager);
            _toolManager.RegisterTool(brushTool);
            
            _toolPanel = new ToolPanelView(new ITool[] { brushTool });

            _keyboardInputHandler = new EditorKeyboardInputHandler(_commandManager, _toolManager);
            _mouseDragHandler = new EditorMouseDragHandler();
            CreateNewMap();
            EditorApplication.update += EditorUpdate;
        }
        
        private void OnDisable()
        {
            EditorApplication.update -= EditorUpdate;
        }

        
        private void EditorUpdate()
        {
            double currentTime = EditorApplication.timeSinceStartup;
            if (currentTime - _lastDrawTime >= FrameTime)
            {
                _lastDrawTime = currentTime;
                Repaint();
            }
        }


        private void OnGUI()
        {
            _keyboardInputHandler.HandleInput();
            
            var maps = FileManager.GetFilesInDirectory(_mapsFullDirectory,PathNames.Extensions.Json);
            _mapSelectionPanel.Draw(maps);
            _panAndZoom.HandleEvents(_gridView.Bounds);
            
            DrawRenameField(); 
            
            
            _settingsView.Draw();
            _gridView.Draw();
            _settingsView.SelectedCellPos = _gridView.SelectedCellPos;
            if(_toolManager.Current != null) _toolPanel.Draw(_toolManager.Current);

            if (GUILayout.Button("Save Map"))
                SaveToJson();
        }

        private void InitGrid()
        {
            for (int x = 0; x < GridConstants.GridWidth; x++)
            for (int y = 0; y < GridConstants.GridHeight; y++)
            {
                _mapData.Grid[x, y] = new GridCell
                {
                    Position = new Vec2Int(x, y),
                    GridType = GridType.Empty
                };
                _mapData.Grid[x,y].EnsureInitialized();
            }
                    
        }
        

        private void DrawRenameField()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Map Name", EditorStyles.boldLabel, GUILayout.Width(100));

            EditorGUI.BeginChangeCheck();
            _mapData.mapId = EditorGUILayout.TextField(_mapData.mapId, GUILayout.Width(300));
            GUILayout.EndHorizontal();
            
        }


        private void SaveToJson()
        {
            _mapData.cells.Clear();
            _mapData.UpdateCellList();

            string fileName = _mapData.mapId + PathNames.Extensions.Json;
            string location = PathHelper.Combine(_mapsFullDirectory, fileName);
            JsonHelper.Save(location, _mapData, false);
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
            _mapData.UpdateGrid();
            InitGrid();
            
            _gridView = new GridView(
                _mapData.Grid,
                _panAndZoom.Zoom,
                _panAndZoom.PanOffset,
                GridConstants.TileWidth, 
                GridConstants.TileHeight,
                _toolManager,
                _mouseDragHandler
            );
            _settingsView.SetMapData(_mapData);
            Repaint();
        }
        private void LoadFromJson(string mapIdWithExtension)
        {
            var mapId = Path.GetFileNameWithoutExtension(mapIdWithExtension);
            _mapData = MapLoader.LoadMap(mapId);
            
            _gridView = new GridView(
                _mapData.Grid,
                _panAndZoom.Zoom,
                _panAndZoom.PanOffset,
                GridConstants.TileWidth,
                GridConstants.TileHeight,
                _toolManager,
                _mouseDragHandler
            );
            
            _settingsView.SetMapData(_mapData);
            Repaint();
        }
        
    }
}

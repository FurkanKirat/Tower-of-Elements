using Core.Commands;
using Core.Constants;
using Core.Entities;
using Core.GridSystem;
using Core.Map;
using Core.Math;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Editor.Helpers;
using Editor.MapEditor.Commands;
using Editor.MapEditor.Windows;
using Managers.Log;

namespace Editor.MapEditor.Views
{
    public class SettingsPanelView
    {
        public Vec2Int? SelectedCellPos { get; set; }

        private MapData _mapData;
        private string _newEnemyId = "basic";
        private float _newEnemyDelay = 1f;

        private ReorderableList _layerList;
        private GridCell _lastCellForList;
        
        private readonly CommandManager _commandManager;

        public SettingsPanelView(CommandManager commandManager)
        {
            _commandManager = commandManager;
        }
        public void SetMapData(MapData mapData)
        {
            _mapData = mapData;
        }

        public void Draw()
        {
            GUILayout.Space(10);
            GUILayout.Label("Map Settings", EditorStyles.boldLabel);
            
            GUILayout.Label("Selected Cell: " + SelectedCellPos);
            
            if (_mapData == null || SelectedCellPos == null) return;
            var cell = _mapData.GetCellFromGrid(SelectedCellPos.Value);
            if (_lastCellForList == null || !cell.Position.Equals(_lastCellForList.Position))
            {
                SetupLayerList(cell.EditableSpriteLayers);
                _lastCellForList = cell;
            }

            GUILayout.Space(10);
            GUILayout.Label("Sprite Layers", EditorStyles.boldLabel);
            _layerList?.DoLayoutList();

            // Enemy Spawn
            GUILayout.Space(10);
            GUILayout.Label("Enemy Spawn Position", EditorStyles.boldLabel);
            if (GUILayout.Button("Set Spawn to Selected Cell"))
            {
                _mapData.enemySpawn = SelectedCellPos.Value;
            }
            GUILayout.Label($"Current Spawn: {_mapData.enemySpawn}");

            // Enemies
            GUILayout.Space(10);
            GUILayout.Label("Enemies", EditorStyles.boldLabel);

            if (_mapData.enemies.Count == 0)
            {
                GUILayout.Label("- No enemies defined.");
            }
            else
            {
                int? removeIndex = null;
                for (int i = 0; i < _mapData.enemies.Count; i++)
                {
                    var enemy = _mapData.enemies[i];
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"[{i}] ID: {enemy.enemyId}  Delay: {enemy.spawnDelay}");
                    if (GUILayout.Button("Remove", GUILayout.Width(60)))
                    {
                        removeIndex = i;
                    }
                    GUILayout.EndHorizontal();
                }
                if (removeIndex.HasValue)
                {
                    _mapData.enemies.RemoveAt(removeIndex.Value);
                }
            }

            GUILayout.Space(10);
            GUILayout.Label("Add Enemy", EditorStyles.boldLabel);
            _newEnemyId = EditorGUILayout.TextField("Enemy ID", _newEnemyId);
            _newEnemyDelay = EditorGUILayout.FloatField("Spawn Delay", _newEnemyDelay);
            if (GUILayout.Button("Add Enemy at Spawn"))
            {
                var newEnemy = new EnemyData
                {
                    enemyId = _newEnemyId,
                    spawnDelay = _newEnemyDelay,
                };
                _mapData.enemies.Add(newEnemy);
            }
            
            
        }

        private void SetupLayerList(SpriteLayer[] layers)
        {
            _layerList = new ReorderableList(layers, typeof(SpriteLayer), false, true, false, false);

            
            _layerList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Sprite Layers");
            };

            _layerList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                layers[index] ??= new SpriteLayer();
                var layer = layers[index];

                float labelWidth = 80f;
                float w = rect.width / 3;

                var label = LayerConstants.LayerNames.Length > index
                    ? LayerConstants.LayerNames[index]
                    : $"Layer {index}";

                EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, EditorGUIUtility.singleLineHeight), $"[{label}]");

                if (LayerConstants.EditableInEditor.Contains(index))
                {
                    if (GUI.Button(new Rect(rect.x + labelWidth + 5, rect.y, w - labelWidth, EditorGUIUtility.singleLineHeight),
                            string.IsNullOrEmpty(layer.SpriteId) ? "Select..." : layer.SpriteId))
                    {
                        string group = LayerConstants.LayerNames[index].ToLower(); // "ground", "path"...

                        SpriteSelectorPopupWindow.Show(
                            selectedId =>
                            {
                                var command = new SetSpriteLayerCommand(
                                    _lastCellForList, index, selectedId, layer.Direction
                                );
                                _commandManager.ExecuteCommand(command);
                            },
                            sprite => sprite.name.Contains(group)
                        );
                    }

                    Direction newDirection = (Direction)EditorGUI.EnumPopup(
                        new Rect(rect.x + w + 5, rect.y, w - 5, EditorGUIUtility.singleLineHeight),
                        layer.Direction
                    );
                    if (newDirection != layer.Direction)
                    {
                        var command = new SetSpriteLayerCommand(
                            _lastCellForList, index, layer.SpriteId, newDirection
                        );
                        _commandManager.ExecuteCommand(command);
                    }
                }

                else
                {
                    EditorGUI.LabelField(new Rect(rect.x + labelWidth + 5, rect.y, w - labelWidth, EditorGUIUtility.singleLineHeight),
                        "(Runtime)");
                }

                if (!string.IsNullOrEmpty(layer.SpriteId))
                {
                    var sprite = TileSpriteDatabase.Get(layer.SpriteId);
                    if (sprite != null)
                    {
                        GUI.DrawTexture(
                            new Rect(rect.x + 2 * w + 10, rect.y, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight),
                            sprite.texture,
                            ScaleMode.ScaleToFit
                        );
                    }
                }
            };
        }
    }
}

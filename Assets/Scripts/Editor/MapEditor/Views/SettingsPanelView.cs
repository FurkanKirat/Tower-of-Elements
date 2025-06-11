using Core.Entities;
using Core.GridSystem;
using Core.Map;
using Core.Math;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using Editor.Helpers;

namespace Editor.MapEditor.Views
{
    public class SettingsPanelView
    {
        public string SelectedSpriteId { get; set; }
        public int SelectedLayerIndex { get; set; }
        public Direction SelectedDirection { get; set; }
        public Vec2Int? SelectedCellPos { get; set; }

        private MapData _mapData;
        private string _newEnemyId = "basic";
        private float _newEnemyDelay = 1f;

        private ReorderableList _layerList;
        private GridCell _lastCellForList;

        public void SetMapData(MapData mapData)
        {
            _mapData = mapData;
        }

        public void Draw()
        {
            GUILayout.Space(10);
            GUILayout.Label("Map Settings", EditorStyles.boldLabel);

            GUILayout.Label("Selected Sprite ID: " + (SelectedSpriteId ?? "(None)"));
            GUILayout.Label("Selected Cell: " + SelectedCellPos);
            GUILayout.Label("Layer Index: " + SelectedLayerIndex);
            GUILayout.Label("Direction: " + SelectedDirection);

            GUILayout.BeginHorizontal();
            if (GUILayout.Toggle(SelectedDirection == Direction.Up, "↑", "Button")) SelectedDirection = Direction.Up;
            if (GUILayout.Toggle(SelectedDirection == Direction.Right, "→", "Button")) SelectedDirection = Direction.Right;
            if (GUILayout.Toggle(SelectedDirection == Direction.Down, "↓", "Button")) SelectedDirection = Direction.Down;
            if (GUILayout.Toggle(SelectedDirection == Direction.Left, "←", "Button")) SelectedDirection = Direction.Left;
            GUILayout.EndHorizontal();

            if (_mapData == null || SelectedCellPos == null) return;
            
            var cell = _mapData.GetCell(SelectedCellPos.Value);
            if (cell != _lastCellForList)
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
            
            if (GUI.changed)
                GUIUtility.ExitGUI();
        }

        private void SetupLayerList(List<SpriteLayer> layers)
        {
            _layerList = new ReorderableList(layers, typeof(SpriteLayer), true, true, true, true);

            _layerList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Sprite Layers");
            };

            _layerList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var layer = layers[index];
                float w = rect.width / 3;

                layer.SpriteId = EditorGUI.TextField(new Rect(rect.x, rect.y, w, EditorGUIUtility.singleLineHeight), layer.SpriteId);
                layer.Direction = (Direction)EditorGUI.EnumPopup(new Rect(rect.x + w + 5, rect.y, w - 5, EditorGUIUtility.singleLineHeight), layer.Direction);

                if (layer.SpriteId == null) return;
                var sprite = TileSpriteDatabase.Get(layer.SpriteId);
                if (sprite != null)
                {
                    GUI.DrawTexture(new Rect(rect.x + 2 * w + 10, rect.y, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight), sprite.texture, ScaleMode.ScaleToFit);
                }
            };
        }
    }
}

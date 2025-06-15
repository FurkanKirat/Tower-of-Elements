using Core.Constants;
using Core.GridSystem;
using Editor.Helpers;
using Editor.MapEditor.Tools;
using Editor.MapEditor.Windows;
using Managers.Log;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor.Views
{
    public class BrushToolView : IToolView
    {
        private readonly BrushTool _brushTool;

        public BrushToolView(BrushTool brushTool)
        {
            _brushTool = brushTool;
        }

        public void Draw()
        {
            GUILayout.Space(10);
            GUILayout.Label("🖌️ Brush Tool", EditorStyles.boldLabel);

            float labelWidth = 80f;
            float totalWidth = EditorGUIUtility.currentViewWidth - 50f;
            float w = totalWidth / 3f;

            int index = _brushTool.SelectedLayerIndex;
            string label = LayerConstants.LayerNames.Length > index
                ? LayerConstants.LayerNames[index]
                : $"Layer {index}";

            Rect rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(new Rect(rect.x, rect.y, labelWidth, rect.height), $"[{label}]");

            if (LayerConstants.EditableInEditor.Contains(index))
            {
                if (GUI.Button(new Rect(rect.x + labelWidth + 5, rect.y, w - labelWidth, rect.height),
                        string.IsNullOrEmpty(_brushTool.SelectedSpriteId) ? "Select..." : _brushTool.SelectedSpriteId))
                {
                    string group = LayerConstants.LayerNames[index].ToLower();

                    GameLogger.Log("dsafdafssdaf");
                    SpriteSelectorPopupWindow.Show(
                        selectedId => { _brushTool.SelectedSpriteId = selectedId; },
                        sprite => sprite.name.StartsWith(group)
                    );
                }

                _brushTool.SelectedDirection = (Direction)EditorGUI.EnumPopup(
                    new Rect(rect.x + w + 5, rect.y, w - 5, rect.height),
                    _brushTool.SelectedDirection
                );
            }
            else
            {
                EditorGUI.LabelField(new Rect(rect.x + labelWidth + 5, rect.y, w - labelWidth, rect.height), "(Runtime)");
            }

            if (!string.IsNullOrEmpty(_brushTool.SelectedSpriteId))
            {
                var sprite = TileSpriteDatabase.Get(_brushTool.SelectedSpriteId);
                if (sprite != null)
                {
                    GUI.DrawTexture(
                        new Rect(rect.x + 2 * w + 10, rect.y, rect.height, rect.height),
                        sprite.texture,
                        ScaleMode.ScaleToFit
                    );
                }
            }

            _brushTool.SelectedLayerIndex = EditorGUILayout.IntSlider("Layer", _brushTool.SelectedLayerIndex, 0, LayerConstants.LayerNames.Length - 1);
        }
    }
}

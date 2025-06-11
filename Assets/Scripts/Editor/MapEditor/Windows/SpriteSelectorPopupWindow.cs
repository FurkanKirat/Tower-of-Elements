using System;
using System.Collections.Generic;
using Editor.Helpers;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor.Windows
{
    public class SpriteSelectorPopupWindow : EditorWindow
    {
        private Vector2 _scrollPosition;
        private IEnumerable<Sprite> _sprites;
        private Action<string> _onSpriteSelected;

        public static void Show(Action<string> onSpriteSelected)
        {
            var window = GetWindow<SpriteSelectorPopupWindow>(true, "Select Sprite", true);
            window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 600, 200);
            window._onSpriteSelected = onSpriteSelected;
            window._sprites = TileSpriteDatabase.All.Values;
            window.ShowUtility();
        }

        private void OnGUI()
        {
            GUILayout.Label("Click a sprite to select", EditorStyles.boldLabel);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            int itemsPerRow = Mathf.FloorToInt(position.width / 72);
            int column = 0;

            EditorGUILayout.BeginHorizontal();

            foreach (var sprite in _sprites)
            {
                if (sprite == null) continue;

                if (GUILayout.Button(new GUIContent(sprite.texture), GUILayout.Width(64), GUILayout.Height(64)))
                {
                    _onSpriteSelected?.Invoke(sprite.name);
                    Close();
                }

                column++;
                if (column >= itemsPerRow)
                {
                    column = 0;
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }
    }
}
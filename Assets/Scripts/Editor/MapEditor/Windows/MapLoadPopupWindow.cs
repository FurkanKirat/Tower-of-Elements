using System;
using System.Collections.Generic;
using Managers;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor.Windows
{
    public class MapLoadPopupWindow : EditorWindow
    {
        private List<string> _mapIds;
        private Vector2 _scroll;
        private Action<string> _onMapSelected;

        public static void Show(List<string> mapIds, Action<string> onMapSelected)
        {
            var window = GetWindow<MapLoadPopupWindow>(true, "Select Map", true);
            window._mapIds = mapIds;
            window._onMapSelected = onMapSelected;
            window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 300, 300);
            window.ShowUtility();
        }

        private void OnGUI()
        {
            GUILayout.Label("Available Maps", EditorStyles.boldLabel);
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            foreach (var fullPath in _mapIds)
            {
                string fileName = PathHelper.GetFileNameWithoutExtension(fullPath);
                if (GUILayout.Button(fileName, GUILayout.Height(30)))
                {
                    _onMapSelected?.Invoke(fullPath);
                    Close();
                }
            }

            EditorGUILayout.EndScrollView();
        }
    }
}
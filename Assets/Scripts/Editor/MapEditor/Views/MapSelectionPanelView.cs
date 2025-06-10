using Editor.MapEditor.Windows;
using System.Collections.Generic;
using UnityEngine;
namespace Editor.MapEditor.Views
{
    public class MapSelectionPanelView
    {
        private readonly System.Action _onNewMapClicked;
        private readonly System.Action<string> _onMapLoadClicked;

        public MapSelectionPanelView(System.Action onNewMapClicked, System.Action<string> onMapLoadClicked)
        {
            _onNewMapClicked = onNewMapClicked;
            _onMapLoadClicked = onMapLoadClicked;
        }

        public void Draw(List<string> availableMapIds)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("New Map", GUILayout.Width(100)))
                _onNewMapClicked?.Invoke();

            GUILayout.Space(10);
            if (GUILayout.Button("Load Map", GUILayout.Width(100)))
            {
                MapLoadPopupWindow.Show(availableMapIds, _onMapLoadClicked);
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
        
    }

}
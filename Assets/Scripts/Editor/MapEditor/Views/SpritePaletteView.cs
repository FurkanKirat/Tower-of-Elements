using Editor.MapEditor.Windows;
using UnityEditor;
using UnityEngine;

namespace Editor.MapEditor.Views
{
    public class SpritePaletteView
    {
        private string _selectedSpriteId;

        public string SelectedSpriteId => _selectedSpriteId;

        public string DrawAndSelectSprite()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Selected Sprite:", GUILayout.Width(100));
            GUILayout.Label(_selectedSpriteId ?? "(None)", EditorStyles.textField);
            if (GUILayout.Button("🗑️ Empty (Remove Sprite)"))
            {
                _selectedSpriteId = null;
            }
            if (GUILayout.Button("Select Sprite", GUILayout.Width(120)))
            {
                SpriteSelectorPopupWindow.Show(id =>
                {
                    _selectedSpriteId = id;
                    Debug.Log("Selected Sprite: " + id);
                });
            }
            GUILayout.EndHorizontal();

            return _selectedSpriteId;
        }
    }
    
}
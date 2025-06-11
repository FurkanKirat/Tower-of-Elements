namespace Editor
{
    #if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public static class TileAssetGenerator
{
    private const string spriteFolder = "Assets/Resources/Art/Tiles";
    private const string tileOutputFolder = "Assets/Resources/Tiles";

    [MenuItem("Tools/Tile Generator/Fix Sprites & Generate Tiles")]
    public static void FixSpritesAndGenerateTiles()
    {
        if (!Directory.Exists(tileOutputFolder))
        {
            Directory.CreateDirectory(tileOutputFolder);
            AssetDatabase.Refresh();
        }

        string[] guids = AssetDatabase.FindAssets("t:Texture2D", new[] { spriteFolder });
        int fixedSprites = 0;
        int createdTiles = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

            if (importer == null)
                continue;

            bool changed = false;

            if (importer.textureType != TextureImporterType.Sprite)
            {
                importer.textureType = TextureImporterType.Sprite;
                changed = true;
            }

            if (importer.spriteImportMode != SpriteImportMode.Single)
            {
                importer.spriteImportMode = SpriteImportMode.Single;
                changed = true;
            }

            if (importer.spritePixelsPerUnit != 32)
            {
                importer.spritePixelsPerUnit = 32;
                changed = true;
            }

            if (changed)
            {
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                fixedSprites++;
            }

            // Load sprite and create tile
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (sprite == null)
                continue;

            string tilePath = Path.Combine(tileOutputFolder, sprite.name + ".asset");
            if (!File.Exists(tilePath))
            {
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = sprite;
                tile.colliderType = Tile.ColliderType.None;

                AssetDatabase.CreateAsset(tile, tilePath);
                createdTiles++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[TileAssetGenerator] Fixed {fixedSprites} sprites and created {createdTiles} new tiles.");
    }
}
#endif

}
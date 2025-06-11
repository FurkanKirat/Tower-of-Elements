using Core.Constants;
using Core.GridSystem;
using Managers;
using Managers.Log;

namespace Core.Map
{
    using UnityEngine;
    using UnityEngine.Tilemaps;
    using Database;

    public class MapRenderer : MonoBehaviour
    {
        [Header("Tilemap Layers")]
        private Tilemap[] _tilemapLayers;
        [SerializeField] private Grid grid;

        private void Awake()
        {
            _tilemapLayers = grid.GetComponentsInChildren<Tilemap>();
            grid.transform.position = new Vector3(
                -GridConstants.GridWidth / 2f,
                -GridConstants.GridHeight / 2f,
                0
            );
        }
        public void Render(GridCell[,] mapGrid)
        {
            ClearAll();
            LogTilemapLayerWarnings();
            
            for (int x = 0; x < mapGrid.GetLength(0); x++)
            {
                for (int y = 0; y < mapGrid.GetLength(1); y++)
                {
                    GridCell cell = mapGrid[x, y];
                    Vector3Int tilePos = new Vector3Int(x, y, 0);

                    for (int i = 0; i < cell.SpriteLayers.Count; i++)
                    {
                        var layer = cell.SpriteLayers[i];
                        if (string.IsNullOrEmpty(layer.SpriteId)) continue;

                        var tile = TileDatabase.Get(layer.SpriteId);
                        LogTileIntegrity(layer.SpriteId, tile);
                        
                        _tilemapLayers[i].SetTile(tilePos, tile);
                        
                        _tilemapLayers[i].SetTransformMatrix(tilePos, Matrix4x4.Rotate(Quaternion.Euler(0, 0, layer.Direction.ToClockwiseDegrees())));
                    }
                }
            }
        }

        public void ClearAll()
        {
            foreach (var tilemap in _tilemapLayers)
            {
                tilemap.ClearAllTiles();
            }
        }
        
        private void LogTilemapLayerWarnings()
        {
            EditorOnly.Execute(() =>
            {
                for (int i = 0; i < _tilemapLayers.Length; i++)
                {
                    if (_tilemapLayers[i] == null)
                        GameLogger.Error($"❌ Tilemap layer [{i}] is null!");
                }
            });
        }

        private void LogTileIntegrity(string spriteId, TileBase tile)
        {
            EditorOnly.Execute(() =>
            {
                if (tile is Tile t)
                {
                    if (t.sprite == null)
                        GameLogger.Error($"❌ Tile {spriteId} exists but has NO SPRITE!");
                }
                else
                {
                    GameLogger.Warn($"Tile {spriteId} is not of type Tile, but {tile.GetType().Name}");
                }
            });
        }

    }


}
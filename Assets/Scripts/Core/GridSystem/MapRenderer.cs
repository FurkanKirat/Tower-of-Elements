namespace Core.GridSystem
{
    using UnityEngine;
    using Database;

    public class MapRenderer : MonoBehaviour
    {
        public GameObject tilePrefab;
        public Transform tileParent;

        public void Render(GridCell[,] grid)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                GridCell cell = grid[x, y];
                Vector2 isoPos = ToIsometric(cell.Position.x, cell.Position.y);

                foreach (var layer in cell.SpriteLayers)
                {
                    var sprite = TileSpriteDatabase.Get($"{layer.SpriteId}_{layer.Direction.ToString().ToLower()}") 
                                 ?? TileSpriteDatabase.Get(layer.SpriteId);

                    if (sprite == null) continue;

                    GameObject go = Instantiate(tilePrefab, tileParent);
                    go.name = $"Tile_{x}_{y}_{layer.SpriteId}";
                    go.transform.localPosition = isoPos;
                    go.GetComponent<SpriteRenderer>().sprite = sprite;
                }
            }
        }

        private Vector2 ToIsometric(int x, int y)
        {
            float tileWidth = 64;
            float tileHeight = 32;
            float screenX = (x - y) * tileWidth / 2f;
            float screenY = (x + y) * tileHeight / 2f;
            return new Vector2(screenX, screenY);
        }
    }

}
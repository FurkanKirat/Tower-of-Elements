namespace Core.Game
{
    using Map;
    using GridSystem;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MapRenderer mapRenderer;
        private MapData _mapData;
        private GridCell[,] _gridCells;

        private void Start()
        {
            LoadMap();
            _gridCells = MapConverter.ToGridArray(_mapData);
            mapRenderer.Render(_gridCells);
        }

        private void LoadMap()
        {
            _mapData = MapLoader.LoadMap("furkan");
        }
    }

}
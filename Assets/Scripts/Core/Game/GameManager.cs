using Core.GridSystem;

namespace Core.Game
{
    using Map;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MapRenderer mapRenderer;
        private MapData _mapData;
        private IGridManager _gridManager;

        private void Start()
        {
            LoadMap();
            _gridManager = new GridManager(_mapData);
            mapRenderer.Render(_mapData.Grid);
        }

        private void LoadMap()
        {
            _mapData = MapLoader.LoadMap("furkan");
        }
    }

}
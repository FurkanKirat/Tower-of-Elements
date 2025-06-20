using System.Linq;
using Core.Database;
using Core.Entities;
using Core.GridSystem;
using Core.Map;
using Core.Tower;
using UI;
using UnityEngine;

namespace Core.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private string mapId;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private MapRenderer mapRenderer;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private GameManager gameManager;
        
        private IGridManager _gridManager;
        private MapData _mapData;
        private IGameContext GameContext { get; set; }
        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            LoadDB();
            LoadMap();
            InitManagers();
            
            mapRenderer.Initialize();
            mapRenderer.Render(_mapData.Grid);
        }

        private void LoadDB()
        {
            TileDatabase.Load();
            TowerDataDatabase.Load();
            TowerAssetDatabase.Load();
        }
        private void LoadMap()
        {
            _mapData = MapLoader.LoadMap(mapId);
        }

        private void InitManagers()
        {
            _gridManager = new GridManager(_mapData);
            
            gameManager.RegisterUpdatable(enemyManager)
                .RegisterUpdatable(towerManager);
            
            GameContext = new GameContext(
                _gridManager, 
                enemyManager, 
                towerManager, 
                uiManager, 
                gameManager
                );
            
            InjectContext();
        }
        
        
        private void InjectContext()
        {
            foreach (var ctxUser in FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IGameContextUser>())
            {
                ctxUser.SetContext(GameContext);
            }
        }

    }
}
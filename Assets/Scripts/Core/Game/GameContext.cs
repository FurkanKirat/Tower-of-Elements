using Core.Entities;
using Core.GridSystem;
using Core.Tower;
using UI;

namespace Core.Game
{
    public class GameContext : IGameContext
    {
        public IGridManager GridManager { get; private set; }
        public IEnemyManager EnemyManager { get; private set; }
        public ITowerManager TowerManager { get; private set; }
        //public IWaveManager WaveManager { get; private set; }
        public IUIManager UIManager { get; private set; }
        public IGameManager GameManager { get; private set; }

        public GameContext(
            IGridManager gridManager,
            IEnemyManager enemyManager,
            ITowerManager towerManager,
            //IWaveManager waveManager,
            IUIManager uiManager,
            IGameManager gameManager
            )
        {
            GridManager = gridManager;
            EnemyManager = enemyManager;
            TowerManager = towerManager;
            //WaveManager = waveManager;
            UIManager = uiManager;
            GameManager = gameManager;
        }
    }

}
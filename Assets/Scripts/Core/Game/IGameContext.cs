using Core.Entities;
using Core.GridSystem;
using Core.Tower;
using UI;

namespace Core.Game
{
    public interface IGameContext
    {
        IGridManager GridManager { get; }
        IEnemyManager EnemyManager { get; }
        TowerManager TowerManager { get; }
        //IWaveManager WaveManager { get; }
        IUIManager UIManager { get; }
        IGameManager GameManager { get; }
    }

}
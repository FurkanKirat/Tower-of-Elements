using System;
using Core.Interfaces;

namespace Core.Game
{
    public interface IGameManager
    {
        GameManager.GameState State { get; }
        event Action<GameManager.GameState> OnGameStateChanged;

        void ChangeState(GameManager.GameState newState);
        void TogglePause();
        IGameManager RegisterUpdatable(IGameUpdatable system);
        IGameManager UnregisterUpdatable(IGameUpdatable system);
    }
}
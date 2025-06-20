using System;
using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Game
{
    using UnityEngine;

    public class GameManager : MonoBehaviour, IGameContextUser, IGameManager
    {
        private IGameContext _gameContext;
        public enum GameState
        {
            WaitingToStart,
            Playing,
            Paused,
            Victory,
            Defeat
        }
        
        public GameState State { get; private set; } = GameState.WaitingToStart;

        public event Action<GameState> OnGameStateChanged;
        
        private readonly List<IGameUpdatable> _updatables = new();

        private void Start()
        {
            ChangeState(GameState.Playing);
        }

        private void Update()
        {
            if (State != GameState.Playing) return;

            foreach (var system in _updatables)
            {
                system.UpdateLogic();
            }
        }
        
        public IGameManager RegisterUpdatable(IGameUpdatable system)
        {
            if (!_updatables.Contains(system))
                _updatables.Add(system);
            return this;
        }

        public IGameManager UnregisterUpdatable(IGameUpdatable system)
        {
            _updatables.Remove(system);
            return this;
        }

        public void ChangeState(GameState newState)
        {
            if (State == newState) return;

            State = newState;
            OnGameStateChanged?.Invoke(newState);
            
            switch (newState)
            {
                case GameState.Paused:
                    Time.timeScale = 0f;
                    break;
                case GameState.Playing:
                    Time.timeScale = 1f;
                    break;
                case GameState.Victory:
                    _gameContext.UIManager.ShowVictoryScreen();
                    break;
                case GameState.Defeat:
                    _gameContext.UIManager.ShowGameOverScreen();
                    break;
            }
        }

        public void TogglePause()
        {
            if (State == GameState.Playing)
                ChangeState(GameState.Paused);
            else if (State == GameState.Paused)
                ChangeState(GameState.Playing);
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }
    }


}
using Core.Game;

namespace Core.Tower
{
    using UnityEngine;

    public class TowerBehaviour : MonoBehaviour, IGameContextUser
    {
        private TowerInstance _instance;
        private IGameContext _gameContext;

        public void Initialize(TowerInstance instance)
        {
            _instance = instance;
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        private void Update()
        {
            if (_instance == null || _gameContext == null)
                return;

            _instance.UpdateLogic(_gameContext);
            
        }

        public TowerInstance GetInstance() => _instance;
    }

}
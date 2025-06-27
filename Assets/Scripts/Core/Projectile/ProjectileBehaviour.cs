using Core.Game;
using UnityEngine;

namespace Core.Projectile
{
    public class ProjectileBehaviour : MonoBehaviour, IGameContextUser
    {
        private ProjectileInstance _instance;
        private IGameContext _gameContext;

        public void Initialize(ProjectileInstance instance)
        {
            _instance = instance;
            var p = instance.Position;
            transform.position = new Vector3(p.x, p.y, transform.position.z);
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
            
            var p = _instance.Position;
            transform.position = new Vector3(p.x, p.y, transform.position.z);

            // If the instance signaled destruction, destroy this GameObject
            if (_instance.IsDestroyed)
            {
                Destroy(gameObject);
            }
            
        }
        
        private void OnDestroy()
        {
            // If this behaviour is destroyed, clear context to avoid leaks
            _instance = null;
            _gameContext = null;
        }

        public ProjectileInstance GetInstance() => _instance;
    }
}
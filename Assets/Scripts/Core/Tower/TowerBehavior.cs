namespace Core.Tower
{
    using UnityEngine;

    public class TowerBehaviour : MonoBehaviour
    {
        private TowerInstance _instance;

        public void Initialize(TowerInstance instance)
        {
            _instance = instance;
        }

        private void Update()
        {
            if (!_instance.IsPlaced)
                return;

            _instance.CurrentCooldown -= Time.deltaTime;

            if (_instance.CurrentCooldown <= 0f)
            {
                TryAttack();
                _instance.CurrentCooldown = 1f / _instance.TowerStats.attackSpeed;
            }
        }

        private void TryAttack()
        {
            // hedef bulma
            var target = FindNearestEnemy();
            if (target != null)
            {
                // projectile fırlat, animasyon oynat, ses çal, vs.
                Debug.Log($"Tower {_instance.TowerId} attacks {target.name}");
            }
        }

        private GameObject FindNearestEnemy()
        {
            // buraya örnek enemy arama mantığı
            return null;
        }
    }

}
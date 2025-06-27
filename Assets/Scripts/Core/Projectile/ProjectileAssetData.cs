using UnityEngine;
namespace Core.Projectile
{
    [CreateAssetMenu(fileName = "NewProjectileAsset", menuName = "Projectile/Projectile Asset")]
    public class ProjectileAssetData : ScriptableObject
    {
        public string projectileId;
        public string assetName;

        public GameObject prefab;
        public GameObject impactEffect;
        public GameObject trailEffect;

        public AudioClip fireSound;
        public AudioClip impactSound;
        
        public bool useObjectPool = true;
    }
}
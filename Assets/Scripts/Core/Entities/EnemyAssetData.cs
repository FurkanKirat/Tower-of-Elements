namespace Core.Entities
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "NewEnemyAsset", menuName = "Enemy/Enemy Asset")]
    
    public class EnemyAssetData:ScriptableObject
    {
        public string enemyId;
        
        public GameObject prefab;
        public Sprite icon;
        
        public AnimationClip spawnAnimation;
        public AnimationClip walkAnimation;
        public AnimationClip attackAnimation;
        public AnimationClip deathAnimation;
        
        public GameObject spawnEffect;
        public GameObject deathEffect;
        
        public AudioClip attackSound;
        //public AudioClip deathSound;
        //public AudioClip spawnSound;
        
        public int goldReward;
        public int xpReward;
        public bool isBoss;
    }
}
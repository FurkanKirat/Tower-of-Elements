namespace Core.Tower
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "NewTowerAsset", menuName = "Tower/Tower Asset")]
    public class TowerAssetData : ScriptableObject
    {
        public string towerId;
        
        public Sprite icon;
        public GameObject prefab;
        public GameObject projectilePrefab;
    
        public AnimatorOverrideController animatorController;
    
        public AnimationClip buildAnimation;
        public AnimationClip attackAnimation;
        public AnimationClip idleAnimation;
        public AnimationClip deathAnimation;
    
        public GameObject buildEffect;
        public GameObject deathEffect;
    
        public AudioClip buildSound;
        public AudioClip attackSound;
        public AudioClip deathSound;
    
        public Vector2Int sizeInTiles;
        public Vector3 placementOffset;
    }

}
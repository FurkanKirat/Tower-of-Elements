using Core.Database;
using UnityEngine;

namespace Core.Tower
{
    public class TowerBuilder : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Transform towerParent;

        [ContextMenu("Build Tower At (5,5)")]
        public void BuildTestTower()
        {
            BuildTower("tower_archer", new Vector2Int(5, 5));
        }
        public void BuildTower(string towerId, Vector2Int gridPos)
        {
            var asset = TowerAssetDatabase.Get(towerId);
            var data = TowerDataDatabase.Get(towerId);
            var instance = new TowerInstance(data, asset);

            Vector3 worldPos = grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
            GameObject go = Instantiate(asset.prefab, worldPos, Quaternion.identity, towerParent);
            go.GetComponent<TowerBehaviour>().Initialize(instance);
        }
    }

}
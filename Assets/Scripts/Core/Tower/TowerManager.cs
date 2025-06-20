using System.Collections.Generic;
using Core.Database;
using Core.Game;
using Core.Interfaces;
using Core.Math;
using UnityEngine;

namespace Core.Tower
{
    public class TowerManager : MonoBehaviour, ITowerManager, IGameContextUser, IGameUpdatable
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Transform towerParent;
        private IGameContext _gameContext;
        private readonly Dictionary<Vector2Int, TowerInstance> _towers = new();
        private readonly Dictionary<Vector2Int, TowerBehaviour> _towerViews = new();
        
        
        [ContextMenu("Build Tower At (5,5)")]
        public void BuildTestTower()
        {
            BuildTower("tower_archer", new Vector2Int(5, 5));
        }
        
        public IReadOnlyDictionary<Vector2Int,TowerInstance> ActiveTowers => _towers;
        public void BuildTower(string towerId, Vector2Int gridPos)
        {
            if (!CanBuild(towerId, gridPos)) return;
            
            var asset = TowerAssetDatabase.Get(towerId);
            var data = TowerDataDatabase.Get(towerId);
            
            var instance = new TowerInstance(data, asset, gridPos.ToVec2Float())
            {
                IsPlaced = true,
            };
            
            Vector3 worldPosCorner = grid.CellToWorld(new Vector3Int(gridPos.x, gridPos.y, 0));
            Vector3 realWorldPos = worldPosCorner - new Vector3(0.5f, 0.5f, 0f);
            GameObject go = Instantiate(asset.prefab, realWorldPos, Quaternion.identity, towerParent);
            
            var towerBehaviour = go.GetComponent<TowerBehaviour>();
            towerBehaviour.Initialize(instance);
            towerBehaviour.SetContext(_gameContext);
            
            _towers.Add(gridPos,instance);
            _towerViews.Add(gridPos,towerBehaviour);
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public bool CanBuild(string towerId, Vector2Int gridPos)
        {
            return _gameContext.GridManager.IsInsideBounds(gridPos) && 
                   _gameContext.GridManager.IsBuildable(gridPos) &&
                   !HasTowerAt(gridPos);
        }

        public bool RemoveTower(Vector2Int gridPos)
        {
            if (!HasTowerAt(gridPos)) return false;
            
            _towers.Remove(gridPos);

            if (_towerViews.TryGetValue(gridPos, out TowerBehaviour view))
            {
                Destroy(view.gameObject);
                _towerViews.Remove(gridPos);
            }

            return true;
        }

        public bool HasTowerAt(Vector2Int gridPos)
        {
            return _towers.ContainsKey(gridPos);
        }

        public void UpdateLogic()
        {
            foreach (var (_, tower) in _towers)
            {
                tower.UpdateLogic(_gameContext);
            }
        }
    }

}
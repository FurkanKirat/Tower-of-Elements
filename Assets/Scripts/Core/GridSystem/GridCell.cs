using System;
using System.Collections.Generic;
using Core.Math;
using Core.Tower;
using Newtonsoft.Json;

namespace Core.GridSystem
{
    [Serializable]
    public class GridCell : IGridCell
    {
        private List<SpriteLayer> _spriteLayers = new();
        
        public GridType GridType { get; set; }
        public Vec2Int Position { get; set;}
        public TowerInstance TowerInstance { get; set;}
        public IReadOnlyList<SpriteLayer> SpriteLayers => _spriteLayers;
        
#if UNITY_EDITOR
        [JsonIgnore]
        public List<SpriteLayer> EditableSpriteLayers => _spriteLayers;
#endif

        
        public void SetLayer(int index, SpriteLayer layer)
        {
            if (_spriteLayers == null)
                _spriteLayers = new List<SpriteLayer>();
            
            while (_spriteLayers.Count <= index)
                _spriteLayers.Add(null);

            _spriteLayers[index] = layer;
        }
        
        public void RecalculateGridType()
        {
            bool hasPath = false;
            bool hasTower = false;

            foreach (var layer in _spriteLayers)
            {
                if (layer == null || string.IsNullOrEmpty(layer.SpriteId))
                    continue;

                if (layer.SpriteId.StartsWith("path"))
                    hasPath = true;
                else if (layer.SpriteId.StartsWith("tower_base"))
                    hasTower = true;
            }

            if (hasPath)
                GridType = GridType.Path;
            else if (hasTower)
                GridType = GridType.Tower;
            else
                GridType = GridType.Empty;
        }

    }

    
}
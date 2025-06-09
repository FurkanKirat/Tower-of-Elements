using System;
using System.Collections.Generic;
using Core.Math;
using Core.Tower;

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
        
        public void SetLayer(int index, SpriteLayer layer)
        {
            if (_spriteLayers == null)
                _spriteLayers = new List<SpriteLayer>();
            
            while (_spriteLayers.Count <= index)
                _spriteLayers.Add(new SpriteLayer());

            _spriteLayers[index] = layer;
        }
    }

    
}
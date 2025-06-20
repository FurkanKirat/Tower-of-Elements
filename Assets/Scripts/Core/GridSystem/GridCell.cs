using System;
using System.Collections.Generic;
using Core.Constants;
using Core.Math;
using Core.Tower;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.GridSystem
{
    [Serializable]
    public class GridCell : IGridCell
    {
        [JsonProperty("SpriteLayers")]
        private SpriteLayer[] _spriteLayers = new SpriteLayer[LayerConstants.LayerCount];
        
        public GridType GridType { get; set; }
        public Vec2Int Position { get; set;}
        
        [JsonIgnore]
        public IReadOnlyList<SpriteLayer> SpriteLayers => _spriteLayers;
        
#if UNITY_EDITOR
        [JsonIgnore]
        public SpriteLayer[] EditableSpriteLayers => _spriteLayers;
#endif

        
        public void SetLayer(int index, SpriteLayer layer)
        {
            if (_spriteLayers == null)
                _spriteLayers = new SpriteLayer[LayerConstants.LayerCount];

            if (index < 0 || index >= LayerConstants.LayerCount)
                return;

            _spriteLayers[index] = layer;
        }
        
        public void RecalculateGridType()
        {
            if (_spriteLayers[LayerConstants.Path]?.SpriteId != null)
            {
                GridType = GridType.Path;
                return;
            }
            
            var groundId = _spriteLayers[LayerConstants.Ground]?.SpriteId;

                
            if (groundId != null && groundId.StartsWith(TileConstants.TowerGroundPrefix))
                GridType = GridType.Buildable;
            else
                GridType = GridType.Empty;
        }
        
        public void EnsureInitialized()
        {
            if (_spriteLayers == null || _spriteLayers.Length != LayerConstants.LayerCount)
                _spriteLayers = new SpriteLayer[LayerConstants.LayerCount];

            for (int i = 0; i < _spriteLayers.Length; i++)
            {
                if (_spriteLayers[i] == null)
                    _spriteLayers[i] = new SpriteLayer();
            }
        }
        
        public override string ToString()
        {
            return
                $"{nameof(_spriteLayers)}: {_spriteLayers}, {nameof(GridType)}: {GridType}, {nameof(Position)}: {Position}";
        }
    }

    
}
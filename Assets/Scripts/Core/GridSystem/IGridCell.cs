#nullable enable
using System.Collections.Generic;
using Core.Math;
using Core.Tower;

namespace Core.GridSystem
{
    public interface IGridCell
    {
        GridType GridType { get; }
        Vec2Int Position { get; }
        IReadOnlyList<SpriteLayer> SpriteLayers { get; }

    }
    
   
    
    
}
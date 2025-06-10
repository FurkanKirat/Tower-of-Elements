using System;

namespace Core.GridSystem
{
    [Serializable]
    public class SpriteLayer
    {
        public string SpriteId { get; set; }
        public Direction Direction { get; set; }
    }
}
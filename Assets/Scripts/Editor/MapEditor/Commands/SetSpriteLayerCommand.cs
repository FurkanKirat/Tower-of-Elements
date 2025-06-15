using Core.Commands;
using Core.GridSystem;

namespace Editor.MapEditor.Commands
{
    public class SetSpriteLayerCommand : ICommand
    {
        private readonly GridCell _cell;
        private readonly int _layerIndex;
        private readonly string _newSpriteId;
        private readonly Direction _newDirection;

        private readonly string _oldSpriteId;
        private readonly Direction _oldDirection;

        public SetSpriteLayerCommand(GridCell cell, int layerIndex, string newSpriteId, Direction newDirection)
        {
            _cell = cell;
            _layerIndex = layerIndex;
            _newSpriteId = newSpriteId;
            _newDirection = newDirection;

            var oldLayer = cell.SpriteLayers[layerIndex];
            _oldSpriteId = oldLayer?.SpriteId;
            _oldDirection = oldLayer?.Direction ?? Direction.Up;
        }

        public void Execute()
        {
            _cell.SetLayer(_layerIndex, new SpriteLayer
            {
                SpriteId = _newSpriteId,
                Direction = _newDirection
            });
        }

        public void Undo()
        {
            _cell.SetLayer(_layerIndex, new SpriteLayer
            {
                SpriteId = _oldSpriteId,
                Direction = _oldDirection
            });
        }
    }
}
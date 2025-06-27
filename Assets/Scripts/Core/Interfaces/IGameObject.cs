
using Core.Game;
using Core.Physics;

namespace Core.Interfaces
{
    public interface IGameObject
    {
        public int InstanceId { get; set; }
        public AABB Hitbox { get;  }
        
        void UpdateLogic(IGameContext gameContext);
        void OnRemoved();
    }
}
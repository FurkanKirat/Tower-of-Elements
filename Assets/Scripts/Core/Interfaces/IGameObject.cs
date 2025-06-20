
using Core.Game;
using Core.Physics;

namespace Core.Interfaces
{
    public interface IGameObject
    {
        public int InstanceId { get; set; }
        public AABB Hitbox { get; set; }
        
        void UpdateLogic(IGameContext gameContext);
        void OnDestroy();
    }
}
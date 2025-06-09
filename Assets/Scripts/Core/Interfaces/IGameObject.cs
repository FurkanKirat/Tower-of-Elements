
using Core.Physics;

namespace Core.Interfaces
{
    public interface IGameObject
    {
        public int InstanceId { get; set; }
        public AABB Hitbox { get; set; }
        
        void UpdateLogic();
        void OnDestroy();
    }
}
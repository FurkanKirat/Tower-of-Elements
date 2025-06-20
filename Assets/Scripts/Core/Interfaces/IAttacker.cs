using Core.Game;

namespace Core.Interfaces
{
    public interface IAttacker
    {
        float CurrentCooldown { get; }
        float AttackSpeed { get; }
        float Range { get; }

        void UpdateAttack(IGameContext context);
    }

}
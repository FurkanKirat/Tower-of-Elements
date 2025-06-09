using System;

namespace Core.Tower
{
    [Serializable]
    public class TowerBonusStats
    {
        public float bonusAttack = 0;
        public float bonusDefense = 0;
        public float attackSpeedMultiplier = 1f;

        public void Reset()
        {
            bonusAttack = 0;
            bonusDefense = 0;
            attackSpeedMultiplier = 1f;
        }

        public void Add(TowerBonusStats other)
        {
            bonusAttack += other.bonusAttack;
            bonusDefense += other.bonusDefense;
            attackSpeedMultiplier *= other.attackSpeedMultiplier;
        }

        public TowerBonusStats Clone()
        {
            return new TowerBonusStats
            {
                bonusAttack = this.bonusAttack,
                bonusDefense = this.bonusDefense,
                attackSpeedMultiplier = this.attackSpeedMultiplier
            };
        }
    }

}
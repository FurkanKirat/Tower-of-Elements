using System;

namespace Core.Entities
{
    public class EnemyStats
    {
        public bool isAlive;
        public int maxHealth;
        public int currentHealth;
        public float movementSpeed;
        public int damage;
        public int critDamage;
        public float critChance;
        public bool isCrit;
        public bool isFlying;
        public bool isStunned;

        private static readonly System.Random rand = new System.Random();
        
        
        public void TakeDamage(int damage)
        {
            ApplyCritChance();
            int finalDamage = damage;
            if (isCrit)
            {
                finalDamage = (int)(damage * (critDamage / 100f));
            }

            currentHealth -= finalDamage;
            EnemyDie();
        }

        public void ApplyCritChance()
        {
            isCrit = critChance >= rand.NextDouble();
        }
        
        public void EnemyDie()
        {
            if (currentHealth <= 0)
            {
                isAlive = false;
            }
        }

        public void SlowEffect(float slowingFactor, double slowingDuration)
        {
            movementSpeed *= slowingFactor;
            //slowDuration unity içinde yapılabilir
        }

        public void Heal(int heal)
        {
            if (isAlive!)
            {
                currentHealth += heal;
                currentHealth = System.Math.Min(currentHealth, maxHealth);
            }
        }

        public void StunEnemy()
        {
            isStunned = true;
        }
        
        
        
    }
       
}
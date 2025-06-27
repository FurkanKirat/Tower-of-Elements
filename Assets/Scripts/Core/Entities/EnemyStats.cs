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
        
        
        public void takeDamage(int damage)
        {
            applyCritChance();
            if (isCrit)
            {
                currentHealth -= critDamage;
            }
            else
            {
                currentHealth -= damage;
            }
            enemyDie();
        }

        public void applyCritChance()
        {
            Random rand = new Random();
            isCrit = critChance >= rand.NextDouble();
        }
        
        public void enemyDie()
        {
            if (currentHealth <= 0)
            {
                isAlive = false;
            }
        }

        public void slowEffect(float slowingFactor, double slowingDuration)
        {
            movementSpeed *= slowingFactor;
            //slowDuration unity içinde yapılabilir
        }

        public void heal(int heal)
        {
            if (isAlive!)
            {
                currentHealth += heal;
                currentHealth = System.Math.Min(currentHealth, maxHealth);
            }
        }

        public void stunEnemy()
        {
            isStunned = true;
        }
        
        
        
    }
       
}
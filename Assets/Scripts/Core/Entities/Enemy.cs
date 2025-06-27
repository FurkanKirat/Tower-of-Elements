using System;
using System.Collections;
using UnityEngine;
using Core.Interfaces;
using Core.Entities;
using Core.Game;
using Core.Math;
using Core.Physics;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyStats enemyStats;
    private Vector3[] path;
    private int currentPathIndex = 0;

    private bool isStunned = false;
    private float stunTimer = 0f;

    private bool isSlowed = false;
    private float slowTimer = 0f;
    private float slowMultiplier = 1f;

    private Coroutine poisonCoroutine;

    public EnemyStats EnemyStats => enemyStats;

    public event Action<IEnemy> OnDeath;

    public void InitializePath(Vector3[] pathPoints)
    {
        path = pathPoints;
        transform.position = path[0];
    }

    private void Update()
    {
        HandleStatusTimers();

        if (!isStunned)
            MoveAlongPath();
    }

    private void HandleStatusTimers()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
                isStunned = false;
        }

        if (isSlowed)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                isSlowed = false;
                slowMultiplier = 1f;
            }
        }
    }

    public void MoveAlongPath()
    {
        if (path == null || currentPathIndex >= path.Length) return;

        float speed = EnemyStats.movementSpeed * slowMultiplier;
        Vector3 target = path[currentPathIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            currentPathIndex++;
            if (currentPathIndex >= path.Length)
            {
                OnReachEnd();
            }
        }
    }

    public void OnReachEnd()
    {
        // Hedefe ulaşınca yapılacak işlemler (örnek: base'e zarar)
        Destroy(gameObject);
    }

    public void TakeDamage(float amount, AttackType type)
    {
        enemyStats.currentHealth -= (int)amount;

        if (enemyStats.currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void ApplyStatusEffect(AttackType effect)
    {
        switch (effect)
        {
            case AttackType.Slowing:
                isSlowed = true;
                slowMultiplier = 0.5f;
                slowTimer = 3f;
                break;

            case AttackType.Stunning:
                isStunned = true;
                stunTimer = 2f;
                break;

            case AttackType.Poisoning:
                if (poisonCoroutine != null)
                    StopCoroutine(poisonCoroutine);
                poisonCoroutine = StartCoroutine(ApplyPoisonDamage());
                break;
        }
    }

    private IEnumerator ApplyPoisonDamage()
    {
        int ticks = 3;
        float delay = 1f;

        for (int i = 0; i < ticks; i++)
        {
            TakeDamage(5, AttackType.Poisoning);
            yield return new WaitForSeconds(delay);
        }
    }
    public int InstanceId { get; set; }

    [SerializeField] private Vector2 hitboxSize = new(1f, 1f);
    public AABB Hitbox => new AABB((Vector2)transform.position, hitboxSize);

    public void UpdateLogic(IGameContext gameContext)
    {
        HandleStatusTimers();
        if (!isStunned)
            MoveAlongPath();
    }

    public void OnRemoved()
    {
        Debug.Log($"Enemy {InstanceId} destroyed.");
        // Visual FX, sound, pooling vs.
    }
    public bool IsAlive => enemyStats.currentHealth > 0;

    public Vec2Float Position => new Vec2Float(transform.position.x, transform.position.y);

    public float CurrentHealth => enemyStats.currentHealth;
    public float MaxHealth => enemyStats.maxHealth;

    



}

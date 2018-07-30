using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandardEnemy : Enemy
{
    #region Directions.
    private Vector3 directionToTarget;
    #endregion

    #region Offense.
    [Header("Offense")]
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackCooldown;
    private float attackCooldownTime;

    #region Shooting.
    [SerializeField] private Projectile projectilePrefab;
    private Projectile projectileInstance;

    [SerializeField] private float shootForce;
    #endregion

    #endregion

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Enemy.aiEnabled)
            AIBehaviour();
    }

    protected override void Initialize()
    {
        base.InitializeAI();
    }

    protected override void AIBehaviour()
    {
        if (Vector3.Distance(transform.position, base.target.position) <= attackRadius)
        {
            if (base.enroute)
                base.StopNavigation();

            Attack();
        }
        else
        {
            base.SetDestination();
        }
    }

    protected override void Attack()
    {
        //if (Time.time >= attackCooldownTime)
        //{
        //    directionToTarget = (target.position - transform.position).normalized;

        //    projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        //    projectileInstance.Shoot(directionToTarget, shootForce, "Player");

        //    attackCooldownTime = Time.time + attackCooldown;
        //}
    }

    protected override void Healed()
    {
        Debug.Log("Healed");
    }

    protected override void Damaged()
    {
        Debug.Log("Damaged");
    }

    protected override void Die()
    {
        Debug.Log("Dead");
    }
}
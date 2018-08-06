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
        base.Start();
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

        _stateMachine.ChangeState(States.Walk.Instance);
    }

    protected override void AIBehaviour()
    {
        //if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        //    _stateMachine.ChangeState(States.Walk.Instance);
        //else
        //    _stateMachine.ChangeState(States.Attack.Instance);

        _stateMachine.StateMachineUpdate();
    }

    [SerializeField] private LayerMask obstacleLayer;

    public override void Attack()
    {
        if (Time.time >= attackCooldownTime)
        {
            directionToTarget = (targetPos - transform.position).normalized;

            if (!Physics.Raycast(transform.position, directionToTarget, attackRadius, LayerMap.currentLayerMap.GetLayer("Obstacle")))
            {
                projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectileInstance.Shoot(directionToTarget, shootForce, "Player");

                Debug.DrawRay(transform.position, directionToTarget * attackRadius, Color.red);
            }

            attackCooldownTime = Time.time + attackCooldown;
        }
    }

    protected override void Healed()
    {
        Debug.Log("Healed");
    }

    protected override void Damaged()
    {
        if (health.healthPoints / health.maxHealth < 0.3f)
            _stateMachine.ChangeState(States.Run.Instance);
    }

    protected override void Die()
    {
        Debug.Log("Dead");
        Destroy(this.gameObject);
    }
}
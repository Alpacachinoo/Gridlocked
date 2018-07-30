using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Ai.
    private static bool aiEnabled = true;
    #endregion

    #region Navigation.
    private Transform target;
    [SerializeField] private float speed;

    private bool enroute = false;
    #endregion

    #region Movement & Rotation.
    private Vector3 directionToTarget;
    #endregion

    #region Offense.
    [SerializeField] private float attackRadius;
    [SerializeField] private float attackCooldown;
    private float attackCooldownTime;

    #region Shooting.
    [SerializeField] private Projectile projectilePrefab;
    private Projectile projectileInstance;

    [SerializeField] private float shootForce;
    #endregion

    #endregion

    #region References.
    NavMeshAgent nav;
    #endregion

    public static void ToggleAI(bool toggle)
    {
        aiEnabled = toggle;
    }

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (aiEnabled)
            AIBehaviour();
    }

    private void Initialize()
    {
        ToggleAI(true);

        target = FindObjectOfType<Player>().transform;

        nav.speed = speed;
        nav.stoppingDistance = attackRadius;
        
    }

    private void AIBehaviour()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackRadius)
        {
            if (enroute)
                Stop();

            Attack();
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        nav.speed = speed;
        nav.SetDestination(target.position);
        enroute = true;
    }

    private void Stop()
    {
        nav.ResetPath();
        enroute = false;
    }

    private void Attack()
    {
        if (Time.time >= attackCooldownTime)
        {
            directionToTarget = (target.position - transform.position).normalized;

            projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.Shoot(directionToTarget, shootForce, "Player");

            attackCooldownTime = Time.time + attackCooldown;
        }
    }
}
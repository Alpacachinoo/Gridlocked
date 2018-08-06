using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    #region Ai.
    //Static stuff.
    protected static bool aiEnabled = true;

    public static void ToggleAI(bool toggle)
    {
        aiEnabled = toggle;
    }

    private void OnEnable()
    {
        health.Healed += Healed;
        health.Damaged += Damaged;
        health.Dead += Die;
    }

    private void OnDisable()
    {
        health.Healed -= Healed;
        health.Damaged -= Damaged;
        health.Dead -= Die;
    }
    #endregion

    #region Health
    [Header("Health")]
    public Health health;
    #endregion

    #region Navigation.
    [Header("Navigation")]
    public float speed;
    public float stoppingDistance;

    [SerializeField] public Vector3 targetPos;
    [SerializeField] public bool enroute { get; private set; }
    #endregion

    public StateMachine _stateMachine;

    public Vector3 spawnPos;

    #region References.
    public NavMeshAgent nav;
    #endregion

    #region Overridden functions.
    protected abstract void Initialize();

    protected abstract void AIBehaviour();

    public abstract void Attack();

    protected abstract void Healed();

    protected abstract void Damaged();

    protected abstract void Die();
    #endregion

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    protected void Start()
    {
        spawnPos = transform.position;
    }

    protected void InitializeAI()
    {
        health.Initialize();

        nav.speed = speed;
        nav.stoppingDistance = stoppingDistance;

        _stateMachine = new StateMachine(this);

        ToggleAI(true);
    }

    public void SetDestination()
    {
        nav.SetDestination(targetPos);
        enroute = true;
    }

    public void StopNavigation()
    {
        nav.ResetPath();
        enroute = false;
    }
}
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
    [SerializeField] protected float speed;
    protected float stoppingDistance;

    protected Transform target;
    protected bool enroute = false;
    #endregion

    #region References.
    private NavMeshAgent nav;
    #endregion

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    protected void InitializeAI()
    {
        target = FindObjectOfType<Player>().transform;

        health.Initialize();

        nav.speed = speed;
        nav.stoppingDistance = stoppingDistance;

        ToggleAI(true);
    }

    protected void SetDestination()
    {
        nav.SetDestination(target.position);
        enroute = true;
    }

    protected void StopNavigation()
    {
        nav.ResetPath();
        enroute = false;
    }

    protected abstract void Initialize();

    protected abstract void AIBehaviour();

    protected abstract void Attack();

    protected abstract void Healed();

    protected abstract void Damaged();

    protected abstract void Die();
}

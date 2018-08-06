using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimidEnemy : Enemy
{
    private StateMachine _stateMachine;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        _stateMachine.StateMachineUpdate();
    }

    protected override void Initialize()
    {
        _stateMachine = new StateMachine(this);

        _stateMachine.ChangeState(States.Walk.Instance);
    }

    protected override void AIBehaviour()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Damaged()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override void Healed()
    {
        throw new System.NotImplementedException();
    }
}

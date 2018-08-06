using System.Collections;
using System.Collections.Generic;
using States;

public class StateMachine
{
    private Enemy owner;

    public State currentState { get; private set; }

    public StateMachine(Enemy owner)
    {
        this.owner = owner;
    }

    public void ChangeState(State state)
    {
        if (currentState == state)
            return;

        if (currentState != null)
            currentState.StateExit(this.owner);

        currentState = state;

        currentState.StateEnter(this.owner);
    }

    public void StateMachineUpdate()
    {
        currentState.StateUpdate(this.owner);
    }
}
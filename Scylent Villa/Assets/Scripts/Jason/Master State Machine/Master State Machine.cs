using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterStateMachine
{
    public MasterState CurrentState { get; private set; }

    public void InitializeState(MasterState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(MasterState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

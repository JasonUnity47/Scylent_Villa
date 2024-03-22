using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaidStateMachine
{
    public MaidState CurrentState { get; private set; }

    public void InitializeState(MaidState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(MaidState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

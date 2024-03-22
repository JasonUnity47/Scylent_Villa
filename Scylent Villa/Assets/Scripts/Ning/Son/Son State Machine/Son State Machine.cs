using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonStateMachine
{
    public SonState CurrentState { get; private set; }

    public void InitializeState(SonState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(SonState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

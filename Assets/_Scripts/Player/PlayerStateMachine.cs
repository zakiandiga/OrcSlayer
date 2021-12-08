using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerState CurrentState { get; private set; } //any script that has a reference to currentState can get this variable
    public PlayerState LastState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        LastState = null;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        LastState = CurrentState;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

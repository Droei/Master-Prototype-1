using System;

public class PlayerStateMachine
{
    public event Action<PlayerState> OnStateChanged;

    public PlayerState CurrentState { get; private set; }

    public void SetState(PlayerState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }
}
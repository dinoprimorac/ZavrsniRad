using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState Current { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        Current = startingState;
        Current?.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        Current?.ExitState();
        Current = newState;
        Current.EnterState();
    }

    public void Tick()
    {
        Current?.Tick();
    }

    public void OnAnimationEvent(string eventName)
    {
        Current?.OnAnimationEvent(eventName);
    }
}

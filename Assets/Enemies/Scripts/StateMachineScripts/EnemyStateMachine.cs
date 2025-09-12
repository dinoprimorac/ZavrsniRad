using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState Current { get; private set; }

    public void Initialize(EnemyState startingState)
    {
        Current = startingState;
        Current?.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        if (newState == null || newState == Current) return;
        Current?.Exit();
        Current = newState;
        Current.Enter();
    }

    public void Tick()      => Current?.Tick();
    public void FixedTick() => Current?.FixedTick();
}

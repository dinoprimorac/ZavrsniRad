using UnityEngine;

public abstract class EnemyState
{
    protected readonly EnemyBase enemy;
    protected readonly EnemyStateMachine machine;

    protected EnemyState(EnemyBase e, EnemyStateMachine m) { enemy = e; machine = m; }

    public virtual void Enter() { }
    public virtual void Exit()  { }
    public virtual void Tick()  { }
    public virtual void FixedTick() { }
}

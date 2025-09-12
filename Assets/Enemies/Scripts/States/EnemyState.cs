using UnityEngine;

public class EnemyState
{
    protected readonly Enemy enemy;
    protected readonly EnemyStateMachine stateMachine;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void Tick() { }
    public virtual void OnAnimationEvent(string eventName) { }
}

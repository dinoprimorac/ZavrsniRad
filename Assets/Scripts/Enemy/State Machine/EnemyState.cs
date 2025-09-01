using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected StateMachine stateMachine;

    public EnemyState(Enemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void AnimationTriggerEvent(Enemy.AnimationTriggerType trigger) { }
        
}

using UnityEngine;

public class EnemyStateAttack : EnemyState
{
    public EnemyStateAttack(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void FrameUpdate() { }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType trigger) { }
}

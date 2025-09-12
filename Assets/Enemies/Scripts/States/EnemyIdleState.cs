using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void Tick()
    {
        if (enemy.HasTarget && enemy.IsTargetInDetectionRange())
        {
            stateMachine.ChangeState(new EnemyChaseState(enemy, stateMachine));
        }
    }
}

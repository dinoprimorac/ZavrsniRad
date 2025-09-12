using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyBase e, EnemyStateMachine m) : base(e, m) { }

    public override void Tick()
    {
        if (enemy.HasTarget && enemy.IsTargetInDetectionRange())
            machine.ChangeState(new EnemyChaseState(enemy, machine));
    }
}

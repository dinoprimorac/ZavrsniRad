using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _nextTime;

    public EnemyAttackState(EnemyBase e, EnemyStateMachine m) : base(e, m) { }

    public override void Enter()
    {
        _nextTime = 0f;
        if (enemy.Agent)
        {
            enemy.Agent.isStopped = true;
            enemy.Agent.ResetPath();
        }
    }

    public override void Tick()
    {
        if (!enemy.HasTarget)
        {
            machine.ChangeState(new EnemyIdleState(enemy, machine));
            return;
        }

        if (!enemy.IsInAttackTrigger && enemy.DistanceToTargetXZ() > enemy.AttackRange + 0.05f)
        {
            machine.ChangeState(new EnemyChaseState(enemy, machine));
            return;
        }

        enemy.FaceTargetInstant();

        if (Time.time >= _nextTime && !enemy.IsAttacking)
        {
            _nextTime = Time.time + enemy.AttackCooldown;
            enemy.BeginAttack(); 
        }
    }
}

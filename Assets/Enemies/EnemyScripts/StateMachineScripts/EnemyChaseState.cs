using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private const float Eps = 0.05f;
    private float _stop;

    public EnemyChaseState(EnemyBase e, EnemyStateMachine m) : base(e, m) { }

    public override void Enter()
    {
        if (enemy.Agent)
        {
            _stop = Mathf.Max(enemy.AttackRange, enemy.Agent.radius + 0.1f);
            enemy.Agent.stoppingDistance = _stop;
            enemy.Agent.isStopped = false;
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

        if (!enemy.IsTargetInDetectionRange())
        {
            machine.ChangeState(new EnemyIdleState(enemy, machine));
            return;
        }

        var agent = enemy.Agent;
        if (agent && agent.isOnNavMesh)
        {
            float dist = enemy.DistanceToTargetXZ();
            if (dist > _stop + Eps)
            {
                agent.isStopped = false;
                agent.SetDestination(enemy.Target.position);
            }
            else
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
        }

        if (enemy.IsInAttackTrigger || enemy.DistanceToTargetXZ() <= enemy.AttackRange + Eps)
            machine.ChangeState(new EnemyAttackState(enemy, machine));
    }
}

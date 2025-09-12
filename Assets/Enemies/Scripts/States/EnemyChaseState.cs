using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void EnterState()
    {
        // Optional: lower stopping distance so we get close enough
        if (enemy.Agent) enemy.Agent.stoppingDistance = 0.1f;
    }

    public override void Tick()
    {
        if (!enemy.HasTarget)
        {
            stateMachine.ChangeState(new EnemyIdleState(enemy, stateMachine));
            return;
        }

        // If the player left detection entirely, go idle
        if (!enemy.IsTargetInDetectionRange())
        {
            stateMachine.ChangeState(new EnemyIdleState(enemy, stateMachine));
            return;
        }

        // Move toward target
        if (enemy.Agent && enemy.Agent.isOnNavMesh)
            enemy.Agent.SetDestination(enemy.Target.position);

        // If the player entered the attack trigger, switch to Attack
        if (enemy.IsInAttackTrigger)
            stateMachine.ChangeState(new EnemyAttackState(enemy, stateMachine));
    }

}

using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float _nextAttackTime;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine) : base(enemy, stateMachine) { }

    public override void EnterState()
    {
        _nextAttackTime = 0f;
        if (enemy.Agent) enemy.Agent.ResetPath();
    }

    public override void Tick()
    {
        if (!enemy.HasTarget)
        {
            stateMachine.ChangeState(new EnemyIdleState(enemy, stateMachine));
            return;
        }

        // If the player stepped out of the attack trigger, go back to Chase
        if (!enemy.IsInAttackTrigger)
        {
            stateMachine.ChangeState(new EnemyChaseState(enemy, stateMachine));
            return;
        }

        enemy.FaceTargetInstant();

        if (Time.time >= _nextAttackTime && !enemy.IsAttacking)
        {
            _nextAttackTime = Time.time + enemy.AttackCooldown;
            enemy.BeginAttack();
        }
    }
}

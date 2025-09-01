using UnityEngine;

public class EnemyStateChase : EnemyState
{
    private Transform playerPosition{ get; set; }

    public EnemyStateChase(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enemy Currently in chase State!");
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.Agent.SetDestination(enemy.playerPosition.position);
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType trigger) { }
}

using UnityEngine;

public class EnemyStateIdle : EnemyState
{
    public EnemyStateIdle(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void EnterState()
    { 
        Debug.Log("Enemy Currently in Idle State!");
    }
    public override void ExitState()
    {
        base.ExitState();
     }

    public override void FrameUpdate()
    {
        if (enemy.playerPosition != null)
        {
            enemy.stateMachine.ChangeState(enemy.stateChase);
        }
    }
    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType trigger) { }
}

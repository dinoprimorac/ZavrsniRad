using UnityEngine;

public class StateMachine
{
    public EnemyState CurrentEnemyState { get; set; }
    public void Initialize(EnemyState startState)
    {
        CurrentEnemyState = startState;
        CurrentEnemyState.EnterState();
    }

    public void ChangeState(EnemyState newState)
    {
        CurrentEnemyState.ExitState();
        CurrentEnemyState = newState;
        CurrentEnemyState.EnterState();
    }
}

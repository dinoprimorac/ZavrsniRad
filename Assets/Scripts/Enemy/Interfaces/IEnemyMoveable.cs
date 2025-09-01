using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveable
{
    NavMeshAgent Agent { get; set; }
    bool IsFacingRight { get; set; }
    void MoveEnemy();
}

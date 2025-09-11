using UnityEngine;
using UnityEngine.AI;

public interface IEnemyMoveable
{
    NavMeshAgent agent { get; set; }
    void MoveEnemy();
    
}

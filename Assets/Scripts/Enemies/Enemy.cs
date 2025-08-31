using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyMovement movement;
    public NavMeshAgent agent;
    public EnemyConfig enemyConfig;
    public int health = 100;

    public virtual void OnEnable()
    {
        SetupAgentFromConfig();
    }

    public void OnDisable()
    {
        agent.enabled = false;
    }

    public virtual void SetupAgentFromConfig()
    {
        agent.acceleration = enemyConfig.acceleration;
        agent.angularSpeed = enemyConfig.angularSpeed;
        agent.areaMask = enemyConfig.areaMask;
        agent.avoidancePriority = enemyConfig.avoidancePriority;
        agent.baseOffset = enemyConfig.baseOffset;
        agent.height = enemyConfig.height;
        agent.obstacleAvoidanceType = enemyConfig.obstacleAvoidanceType;
        agent.radius = enemyConfig.radius;
        agent.speed = enemyConfig.speed;
        agent.stoppingDistance = enemyConfig.stoppingDistance;
        movement.updateSpeed = enemyConfig.AIUpdateInterval;

        health = enemyConfig.health;
    }
}

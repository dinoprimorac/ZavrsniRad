using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy configuration", menuName = "SO/Enemy configuration")]
public class EnemyConfig : ScriptableObject
{
    public int health = 100;

    public float AIUpdateInterval = 9.1f;

    public float acceleration = 8.0f;
    public float angularSpeed = 120.0f;
    public int areaMask = -1;
    public int avoidancePriority = 50;
    public float baseOffset = 0;
    public float height = 2.0f;
    public ObstacleAvoidanceType obstacleAvoidanceType;
    public float radius = 0.5f;
    public float speed = 3.0f;
    public float stoppingDistance = 2.0f;
}

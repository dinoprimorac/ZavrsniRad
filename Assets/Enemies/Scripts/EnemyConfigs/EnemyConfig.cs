using UnityEngine;

[CreateAssetMenu(menuName="Enemies/EnemyConfig", fileName="EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Stats")]
    public string displayName = "Enemy";
    public int maxHealth = 30;
    public int damage = 10;

    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float detectionRadius = 12f;

    [Header("Attack")]
    public float attackRange = 1.8f;
    public float attackCooldown = 1.0f;
}

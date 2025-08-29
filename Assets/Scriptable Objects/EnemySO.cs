using UnityEngine;

[CreateAssetMenu(fileName = "Enemy conf", menuName = "ScriptableObject/Enemy stats")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int damage;
    public float moveSpeed;

    public float attackRange;
    public float attackCooldown;
    public float detectionRange;

    public AudioClip attackSound;
    public AudioClip deathSound;

}

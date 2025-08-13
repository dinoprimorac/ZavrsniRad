using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private EnemyStatsSO enemyStats;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = enemyStats.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log(enemyStats.enemyName + " is dead!");
        }
    }
}

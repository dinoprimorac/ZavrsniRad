
using UnityEngine;
public class EnemyKamikaze : MonoBehaviour
{
    private int currentHealth = 0;
    private int maxHealth = 5;
    public string enemyName = "Kamikaze";
    private int damage = 20;

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Dead");
            Destroy(this.gameObject);
            return;
        }
        Debug.Log("Enemy has: " + currentHealth + " left!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kolizija sa igračem");
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

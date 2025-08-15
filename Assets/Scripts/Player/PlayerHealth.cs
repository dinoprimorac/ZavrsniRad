
using UnityEngine;
public class PlayerHealth : MonoBehaviour
{

    private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage and currently has " + currentHealth + " HP!");
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
        UpdateHealthUI();
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        Debug.Log("Player has picked up a health item and now has: " + currentHealth + " HP!");
        UpdateHealthUI();
    }

    private void KillPlayer()
    {
        Debug.Log("Player has died!");
    }

    private void UpdateHealthUI()
    {
        UIManager.Instance.UpdateHealth(currentHealth);
    }
}

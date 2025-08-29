using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int MaxHealth { get; private set; } = 100;
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    public static event Action<int> HealthChanged;
    public static event Action Died;

    private void Awake()
    {
        CurrentHealth = 50;
        IsDead = false;

        HealthChanged?.Invoke(CurrentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);
        HealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        HealthChanged?.Invoke(CurrentHealth);
        Debug.Log("Player healed for " + amount);
    }

    public void Die()
    {
        IsDead = true;
        Debug.Log("Player Died!");
        Died?.Invoke();
    }
}

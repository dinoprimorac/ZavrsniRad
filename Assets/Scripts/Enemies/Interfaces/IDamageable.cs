using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int amount);
    void Die();

    int MaxHealth { get; set; }
    int CurrentHealth { get; set; }

}

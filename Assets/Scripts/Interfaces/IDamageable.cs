using UnityEngine;

public interface IDamageable
{
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void TakeDamage(int amount);
    void Heal(int amount);
    void Die();
    bool IsDead{ get; }
    
}

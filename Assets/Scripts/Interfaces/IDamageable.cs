using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }
    int CurrentHealth { get; }
    int MaxHealth { get; }

    void TakeDamage(int dmgAmount);
}

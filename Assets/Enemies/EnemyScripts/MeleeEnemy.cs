using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [Header("Melee")]
    [SerializeField] private float attackRange = 1.8f;
    [SerializeField] private float attackCooldown = 1.0f;

    public override float AttackRange    => attackRange;
    public override float AttackCooldown => attackCooldown;

    protected override void OnAttackEvent()
    {
        Vector3 center = transform.position + transform.forward * (AttackRange * 0.6f);
        float radius   = Mathf.Max(0.4f, AttackRange * 0.5f);

        var hits = Physics.OverlapSphere(center, radius);
        foreach (var h in hits)
        {
            if (h.transform == Target && h.TryGetComponent<IDamageable>(out var d))
            { d.TakeDamage(Damage); break; }
        }
    }
}

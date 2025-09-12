using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Ranged")]
    [SerializeField] private float attackRange = 12f;
    [SerializeField] private float attackCooldown = 0.8f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float projectileSpeed = 16f;

    // LOS can be re-enabled later once basic firing works
    [SerializeField] private bool requireLineOfSight = false;
    [SerializeField] private LayerMask losBlockers = ~0;

    public override float AttackRange    => attackRange;
    public override float AttackCooldown => attackCooldown;

    protected override void OnAttackEvent()
    {
        if (!projectilePrefab) return;

        Vector3 origin = shootPoint ? shootPoint.position : (transform.position + Vector3.up * 1.0f);
        Vector3 dir = transform.forward;

        if (Target)
        {
            Vector3 aim = (Target.position + Vector3.up * 0.9f) - origin;
            if (aim.sqrMagnitude > 0.0001f) dir = aim.normalized;
        }

        // small offset so we don't spawn inside ourselves
        origin += dir * 0.2f;

        // (Optional) LOS check — disabled by default to guarantee shooting
        if (requireLineOfSight && Target)
        {
            if (Physics.Raycast(origin, dir, out var hit, AttackRange, losBlockers, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.root != transform && hit.transform != Target && hit.transform.root != Target)
                    return; // blocked by environment
            }
        }

        var go = Object.Instantiate(projectilePrefab, origin, Quaternion.LookRotation(dir));
        if (!go.TryGetComponent<Projectile>(out var p)) p = go.AddComponent<Projectile>();
        p.Launch(dir, projectileSpeed, Damage, gameObject);

        Debug.DrawRay(origin, dir * Mathf.Max(AttackRange, 5f), Color.cyan, 1.0f);
    }
}

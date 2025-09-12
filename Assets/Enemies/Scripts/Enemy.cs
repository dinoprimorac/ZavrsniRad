using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private EnemyConfig config;

    [Header("Animation")]
    [SerializeField] private Animator animator;                 // drag the Animator here
    [SerializeField] private string speedParam = "speed";       // must match Controller param
    [SerializeField] private string attackTrigger = "attack";   // must match Controller param

    [Header("Runtime (read-only)")]
    [SerializeField] private Transform target;

    // Backing fields (populated from config)
    private float detectionRadius;
    private float attackRange;
    private float attackCooldown;
    private int damage;

    public EnemyStateMachine Machine { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform Target => target;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    // Animation hashes
    private int _speedHash, _attackHash;
    private bool _isAttacking;

    // ---------- Unity ----------
    protected virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Machine = new EnemyStateMachine();

        if (!animator) animator = GetComponent<Animator>();
        if (!animator) animator = GetComponentInChildren<Animator>();

        _speedHash = Animator.StringToHash(speedParam);
        _attackHash = Animator.StringToHash(attackTrigger);
    }

    protected virtual void Start()
    {
        ApplyConfig();
        Machine.Initialize(new EnemyIdleState(this, Machine));
    }

    protected virtual void Update()
    {
        if (animator)
        {
            float spd = Agent ? Agent.velocity.magnitude : 0f;
            animator.SetFloat(_speedHash, spd);
        }

        Machine.Tick();
    }

    // ---------- Config ----------
    private void ApplyConfig()
    {
        if (config)
        {
            MaxHealth = config.maxHealth;
            CurrentHealth = MaxHealth;
            damage = config.damage;
            detectionRadius = config.detectionRadius;
            attackRange = config.attackRange;
            attackCooldown = config.attackCooldown;

            if (Agent) Agent.speed = config.moveSpeed;
        }
    }

    // ---------- Public helpers for states ----------
    public void SetTarget(Transform t) => target = t;
    public bool HasTarget => target != null;

    public bool IsTargetInDetectionRange()
    {
        if (!target) return false;
        Vector3 d = target.position - transform.position; d.y = 0f;
        return d.sqrMagnitude <= detectionRadius * detectionRadius;
    }

    public float DistanceToTargetXZ()
    {
        if (!target) return float.PositiveInfinity;
        Vector3 d = target.position - transform.position; d.y = 0f;
        return d.magnitude;
    }

    public void FaceTargetInstant()
    {
        if (!target) return;
        Vector3 dir = target.position - transform.position; dir.y = 0f;
        if (dir.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }

    // ---------- Combat / Animation ----------
    public bool IsAttacking => _isAttacking;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
    public int Damage => damage;

    /// Called by AttackState when starting an attack (fires the animation)
    public void BeginAttack()
    {
        if (!animator) return;
        _isAttacking = true;
        animator.ResetTrigger(_attackHash);
        animator.SetTrigger(_attackHash);
    }

    private void PerformMeleeHit()
    {
        // Simple overlap in front of enemy
        Vector3 center = transform.position + transform.forward * (attackRange * 0.6f);
        float radius = Mathf.Max(0.4f, attackRange * 0.5f);

        var hits = Physics.OverlapSphere(center, radius);
        foreach (var h in hits)
        {
            if (h.transform == Target && h.TryGetComponent<IDamageable>(out var dmg))
            {
                dmg.TakeDamage(Damage);
                break;
            }
        }
    }

    // ---------- Health ----------
    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0) Die();
    }

    public void Die() => Destroy(gameObject);

    // ---------- Gizmos ----------
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, config ? config.detectionRadius : 12f);
    }

    // On Enemy (parent)
    public void AnimationEvent_Attack()
    {
        Debug.Log($"[Enemy] HIT on {name}");
        PerformMeleeHit();
    }

    public void AnimationEvent_AttackEnd()
    {
        Debug.Log($"[Enemy] END on {name}");
        _isAttacking = false;
    }

    // Fields / properties
    public bool IsInAttackTrigger { get; private set; }

    // Called by the trigger
    public void SetAttackRangeFlag(bool value) => IsInAttackTrigger = value;


}

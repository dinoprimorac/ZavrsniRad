using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [Header("Data")]
    [SerializeField] private EnemyConfig config;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string speedParam = "speed";
    [SerializeField] private string attackTrigger = "attack";

    [Header("Attack Safety / Debug")]
    [Tooltip("If ON, damage/spawn is driven by Animation Events on Attack.anim. If OFF, we'll simulate hit timing with the fields below.")]
    [SerializeField] private bool useAnimationEvents = false; // default OFF for reliability
    [SerializeField] private float attackAnimLength = 0.9f;
    [SerializeField] private float attackHitTimeA = 0.25f;
    [SerializeField] private float attackHitTimeB = -1f;
    [SerializeField] private float attackMaxDuration = 1.2f;

    [Header("Runtime")]
    [SerializeField] private Transform target;

    public EnemyStateMachine Machine { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Transform Target => target;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public int Damage => config ? config.damage : 10;
    public float DetectionRadius => config ? config.detectionRadius : 12f;

    public bool IsAttacking { get; private set; }
    public bool IsInAttackTrigger { get; private set; }

    public abstract float AttackRange { get; }
    public abstract float AttackCooldown { get; }
    protected abstract void OnAttackEvent(); // what happens at a hit frame

    private int _speedHash, _attackHash;
    private Coroutine _attackCo;

    protected virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Machine = new EnemyStateMachine();

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
        if (animator) animator.SetFloat(_speedHash, Agent ? Agent.velocity.magnitude : 0f);
        Machine.Tick();
    }

    private void ApplyConfig()
    {
        if (!config) return;
        MaxHealth = config.maxHealth;
        CurrentHealth = MaxHealth;
        if (Agent) Agent.speed = config.moveSpeed;
    }

    public void SetTarget(Transform t) => target = t;
    public bool HasTarget => target != null;

    public bool IsTargetInDetectionRange()
    {
        if (!target) return false;
        Vector3 d = target.position - transform.position; d.y = 0f;
        return d.sqrMagnitude <= DetectionRadius * DetectionRadius;
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

    public void SetAttackRangeFlag(bool value) => IsInAttackTrigger = value;

    public void BeginAttack()
    {
        if (IsAttacking || !animator) return;
        IsAttacking = true;

        if (Agent) { Agent.isStopped = true; Agent.ResetPath(); }

        animator.ResetTrigger(_attackHash);
        animator.SetTrigger(_attackHash);

        if (_attackCo != null) StopCoroutine(_attackCo);
        _attackCo = StartCoroutine(useAnimationEvents ? ForceUnlockAfter(attackMaxDuration) : SimulatedAttackRoutine());
    }

    private IEnumerator SimulatedAttackRoutine()
    {
        if (attackHitTimeA >= 0f)
        {
            yield return new WaitForSeconds(attackHitTimeA);
            OnAttackEvent();
        }

        if (attackHitTimeB >= 0f)
        {
            float secondDelay = Mathf.Max(0f, attackHitTimeB - attackHitTimeA);
            if (secondDelay > 0f) yield return new WaitForSeconds(secondDelay);
            else yield return null;
            OnAttackEvent();
        }

        float remain = Mathf.Max(0f, attackAnimLength - Mathf.Max(attackHitTimeA, attackHitTimeB));
        yield return new WaitForSeconds(remain);

        IsAttacking = false;
        _attackCo = null;
    }

    private IEnumerator ForceUnlockAfter(float t)
    {
        yield return new WaitForSeconds(t);
        IsAttacking = false;
        _attackCo = null;
    }

    public void AnimationEvent_Attack() => OnAttackEvent();
    public void AnimationEvent_AttackEnd()
    {
        IsAttacking = false;
        if (_attackCo != null) { StopCoroutine(_attackCo); _attackCo = null; }
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0) return;
        CurrentHealth -= amount;
        if (CurrentHealth <= 0) Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    public void Die()
    {
        
    }
}

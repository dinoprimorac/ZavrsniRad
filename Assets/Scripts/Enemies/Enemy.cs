using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, ITriggerCheckable
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    [SerializeField] private EnemyConfig enemyStats;
    [SerializeField] private NavMeshAgent agent;
    public Transform playerTransform { get; set; }

    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }

    public bool IsAggroed { get; set; } = false;
    public bool IsWithinAttackDistance { get; set; } = false;

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();

        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    private void Start()
    {
        CurrentHealth = enemyStats.maxHealth;
        agent = GetComponent<NavMeshAgent>();

        StateMachine.Initialize(IdleState);

    }

    private void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    // triggeri neprijatelja

    public void SetAggroStatus(bool isAggroed)
    {
        IsAggroed = isAggroed;
    }

    public void SetAttackDistanceBool(bool isWithinAttackDistance)
    {
        IsWithinAttackDistance = isWithinAttackDistance;
    }

    // upravljanje healthom neprijatelja

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    // kretanje neprijatelja

    public void ChasePlayer()
    {
        agent.SetDestination(playerTransform.position);
    }

}

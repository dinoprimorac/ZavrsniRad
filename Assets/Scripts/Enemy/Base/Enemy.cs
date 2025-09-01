using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable
{
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public NavMeshAgent Agent { get; set; }
    public bool IsFacingRight { get; set; }

    public StateMachine stateMachine;
    public EnemyStateIdle stateIdle;
    public EnemyStateChase stateChase;
    public EnemyStateAttack stateAttack;

    public Transform playerPosition { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateIdle = new EnemyStateIdle(this, stateMachine);
        stateChase = new EnemyStateChase(this, stateMachine);
        stateAttack = new EnemyStateAttack(this, stateMachine);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        Agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize(stateIdle);
    }

    private void Update()
    {
        stateMachine.CurrentEnemyState.FrameUpdate();
    }

    public void EnemyAggro(Transform playerPos)
    {
        playerPosition = playerPos;
    }


    public void MoveEnemy()
    {

    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {

    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        Nesto
    }
    


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
        Debug.Log("Died!");
    }


}

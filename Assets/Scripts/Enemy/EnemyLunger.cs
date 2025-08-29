using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyLunger : MonoBehaviour
{
    public Transform player;

    [Header("Chase")]
    public float chaseRange = 18f;
    public float pounceRange = 6f;

    [Header("Behind Landing")]
    public float behindDistance = 2.2f;  
    public float lateralJitter = 0.3f;   
    public float navSampleRadius = 1.0f; 
    public LayerMask losObstacles;

    [Header("Ballistics")]
    public float apexHeight = 2.5f;      
    public float cooldown = 1.6f;

    [Header("Attack")]
    public AttackHitbox attackHitbox;    

    enum State { Idle, Chase, PounceAir, Recover }
    State state = State.Idle;

    NavMeshAgent agent;
    Rigidbody rb;
    float cdTimer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void Update()
    {
        if (!player) return;
        cdTimer -= Time.deltaTime;

        float dist = Vector3.Distance(transform.position, player.position);

        if (state == State.Idle)
        {
            if (dist <= chaseRange) EnterChase();
            return;
        }

        if (state == State.Chase)
        {
            agent.SetDestination(player.position);

            if (dist <= pounceRange && cdTimer <= 0f && HasLineOfSight())
            {
                TryPounce();
            }
            return;
        }
    }

    void EnterChase()
    {
        state = State.Chase;
        if (!agent.enabled) agent.enabled = true;
        agent.isStopped = false;
    }

    bool HasLineOfSight()
    {
        Vector3 eye = transform.position + Vector3.up * 1.2f;
        Vector3 tgt = player.position + Vector3.up * 1.2f;
        Vector3 dir = (tgt - eye);
        float d = dir.magnitude;
        if (d <= 0.05f) return true;
        return !Physics.Raycast(eye, dir / d, d, losObstacles, QueryTriggerInteraction.Ignore);
    }

    void TryPounce()
    {
        // Choose a point BEHIND the player's facing at the moment of takeoff
        Vector3 behind = player.position - player.forward * behindDistance;

        // tiny sideways randomness to avoid head-on body clash
        Vector3 side = Vector3.Cross(Vector3.up, player.forward).normalized;
        behind += side * Random.Range(-lateralJitter, lateralJitter);

        // Snap to NavMesh so the landing is valid
        if (NavMesh.SamplePosition(behind, out NavMeshHit hit, navSampleRadius, NavMesh.AllAreas))
            behind = hit.position;

        // If behind point is too close or identical, nudge further back
        if ((behind - player.position).sqrMagnitude < 0.25f)
            behind = player.position - player.forward * (behindDistance + 0.8f);

        StartCoroutine(PounceRoutine(behind));
    }

    System.Collections.IEnumerator PounceRoutine(Vector3 target)
    {
        state = State.PounceAir;

        // Stop NavMesh and prep
        agent.ResetPath();
        agent.isStopped = true;
        agent.enabled = false;

        // Face target
        Vector3 lookDir = new Vector3(target.x - transform.position.x, 0f, target.z - transform.position.z);
        if (lookDir.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);

        // Compute ballistic velocity to land at 'target' with chosen apexHeight
        Vector3 start = transform.position;
        Vector3 toTarget = target - start;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
        float g = Mathf.Abs(Physics.gravity.y);

        float heightDiff = toTarget.y; // target.y - start.y
        float h = Mathf.Max(apexHeight, heightDiff + 0.5f); // ensure apex above target
        float vy = Mathf.Sqrt(2f * g * h);
        float tUp = vy / g;
        float tDown = Mathf.Sqrt(Mathf.Max(0.0001f, 2f * (h - heightDiff) / g));
        float tTotal = tUp + tDown;

        Vector3 vxz = toTargetXZ / Mathf.Max(0.0001f, tTotal);
        Vector3 initialVel = vxz + Vector3.up * vy;

        // Launch!
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // open attack window during flight
        if (attackHitbox) attackHitbox.BeginWindow();

        rb.linearVelocity = initialVel;

        // Wait for expected flight time (+ small pad)
        float timer = 0f;
        while (timer < tTotal + 0.1f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // close attack window whether we hit or not
        if (attackHitbox) attackHitbox.EndWindow();

        // Landed (or timed out) — restore agent
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Warp agent to actual landing position and resume chase
        agent.Warp(transform.position);
        agent.enabled = true;

        cdTimer = cooldown;
        state = State.Recover;
        yield return new WaitForSeconds(0.15f);
        EnterChase();
    }
}

// EnemyAttackTrigger.cs
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAttackTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
        var col = GetComponent<SphereCollider>();
        col.isTrigger = true;
    }

    // Use the Rigidbody root if present (covers player with child colliders)
    private static Transform RootOf(Collider other) =>
        other.attachedRigidbody ? other.attachedRigidbody.transform : other.transform;

    private void OnTriggerEnter(Collider other)
    {
        var t = RootOf(other);
        if (!t.CompareTag("Player")) return;

        // If enemy had no target yet, set it
        if (!_enemy.HasTarget) _enemy.SetTarget(t);

        // Only flip flag if this is our current target
        if (t == _enemy.Target) _enemy.SetAttackRangeFlag(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var t = RootOf(other);
        if (!t.CompareTag("Player")) return;

        if (_enemy.Target == t)
            _enemy.SetAttackRangeFlag(true); // keep it true while inside
    }

    private void OnTriggerExit(Collider other)
    {
        var t = RootOf(other);
        if (!t.CompareTag("Player")) return;

        if (_enemy.Target == t)
            _enemy.SetAttackRangeFlag(false);
    }

    private void OnDisable()
    {
        // Safety: if this object gets disabled, clear the flag so AI returns to chase
        if (_enemy) _enemy.SetAttackRangeFlag(false);
    }

    #if UNITY_EDITOR
    // Optional gizmo for visibility in the editor
    private void OnDrawGizmosSelected()
    {
        var col = GetComponent<SphereCollider>();
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(col.center, col.radius);
    }
    #endif
}

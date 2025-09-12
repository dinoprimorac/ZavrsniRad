using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyAttackTrigger : MonoBehaviour
{
    private EnemyBase _enemy;

    private void Awake()
    {
        _enemy = GetComponentInParent<EnemyBase>();
        var col = GetComponent<SphereCollider>();
        col.isTrigger = true;
    }

    private static Transform RootOf(Collider c) =>
        c.attachedRigidbody ? c.attachedRigidbody.transform : c.transform;

    private void OnTriggerEnter(Collider other)
    {
        var t = RootOf(other);
        if (!t.CompareTag("Player")) return;

        if (!_enemy.HasTarget) _enemy.SetTarget(t);
        if (t == _enemy.Target) _enemy.SetAttackRangeFlag(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var t = RootOf(other);
        if (t == _enemy.Target) _enemy.SetAttackRangeFlag(true);
    }

    private void OnTriggerExit(Collider other)
    {
        var t = RootOf(other);
        if (t == _enemy.Target) _enemy.SetAttackRangeFlag(false);
    }

    private void OnDisable()
    {
        if (_enemy) _enemy.SetAttackRangeFlag(false);
    }
}

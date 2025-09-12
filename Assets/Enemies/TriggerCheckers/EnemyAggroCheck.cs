using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    private EnemyBase _enemy;
    private void Awake() => _enemy = GetComponentInParent<EnemyBase>();

    private static Transform RootOf(Collider c) =>
        c.attachedRigidbody ? c.attachedRigidbody.transform : c.transform;

    private void OnTriggerEnter(Collider other)
    {
        var t = RootOf(other);
        if (t.CompareTag("Player")) _enemy.SetTarget(t);
    }
}

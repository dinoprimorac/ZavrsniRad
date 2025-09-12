using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    private Enemy _enemy;
    private void Awake() => _enemy = GetComponentInParent<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _enemy.SetTarget(other.transform);
    }
}

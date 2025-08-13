using UnityEngine;

public class EnemyRangedProjectile : MonoBehaviour
{
    private int damage = 10;
    private float lifetime = 3.0f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

    }
}

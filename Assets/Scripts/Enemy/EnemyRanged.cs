
using UnityEngine;
public class EnemyRanged : MonoBehaviour
{
    [Header("Detection & Shooting")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private Transform firePoint;

    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float fireCooldown = 0f;
    [SerializeField] private Transform playerTransform;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found in scene!");
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= detectionRadius)
        {
            RotateTowardsPlayer();
            if (fireCooldown <= 0f)
            {
                ShootAtPlayer();
                fireCooldown = fireRate;
            }
        }

        fireCooldown -= Time.deltaTime;
    }

    private void ShootAtPlayer()
    {
        if (projectilePrefab == null || firePoint == null) return;

        Vector3 direction = (playerTransform.position - firePoint.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
        Debug.Log("Enemy shot at player.");
    }

    private void RotateTowardsPlayer()
    {
        transform.LookAt(playerTransform);
    }
}

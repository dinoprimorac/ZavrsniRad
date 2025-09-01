using UnityEngine;

public class AggroRadiusCheck : MonoBehaviour
{
    public Transform playerPosition { get; set; }
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.GetComponent<Transform>();
            enemy.EnemyAggro(playerPosition);
        }
    }
}

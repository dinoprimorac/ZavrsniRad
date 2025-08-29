using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private int healAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().Heal(healAmount);
        }
    }

}

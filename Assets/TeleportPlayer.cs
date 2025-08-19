using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var cc = other.GetComponent<CharacterController>();
            cc.enabled = false;
            other.transform.position = spawnPoint.position;
            cc.enabled = true;
            Debug.Log("Player fell!");
        }
    }
}

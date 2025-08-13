using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnim.SetTrigger("OpenDoor");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        doorAnim.SetTrigger("CloseDoor");
    }

}

using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("OpenDoor");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        doorAnimator.SetTrigger("CloseDoor");
    }

}

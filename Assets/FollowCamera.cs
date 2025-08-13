using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.localPosition;
    }
}

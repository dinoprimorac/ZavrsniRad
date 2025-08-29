
using UnityEngine;
public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationDirection = new Vector3(0.0f, 90.0f, 0.0f);
    [SerializeField] private float rotationSpeed = 1.0f;

    void Update()
    {
        transform.Rotate(rotationSpeed * rotationDirection * Time.deltaTime);
    }
}

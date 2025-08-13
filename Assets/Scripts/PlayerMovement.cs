
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement variables")]
    [SerializeField] private float moveSpeed = 3.0f;
    [Header("Jump variables")]
    [SerializeField] private float jumpHeight = 5.0f;
    [SerializeField] private float gravityMultiplier = 1.0f;
    [Header("Look variables")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float verticalLookLimit = 80.0f;
    [Header("References")]
    [SerializeField] private CharacterController cc;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;

    private Vector3 currentMovement;
    private float verticalRotation;
    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
        cc = GetComponent<CharacterController>();
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }
    public void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }
    private void HandleJumping()
    {
        if (cc.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (playerInputHandler.JumpTriggered)
            {
                currentMovement.y = jumpHeight;
            }
        }
        else
        {
            currentMovement.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }
    }
    private void HandleMovement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * moveSpeed;
        currentMovement.z = worldDirection.z * moveSpeed;
        HandleJumping();
        cc.Move(currentMovement * Time.deltaTime);
    }
    private void ApplyHorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }
    private void ApplyVerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -verticalLookLimit, verticalLookLimit);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    private void HandleRotation()
    {
        float mouseXRotation = playerInputHandler.RotationInput.x * mouseSensitivity;
        float mouseYRotation = playerInputHandler.RotationInput.y * mouseSensitivity;
        ApplyHorizontalRotation(mouseXRotation);
        ApplyVerticalRotation(mouseYRotation);
    }
    
  

    
}


using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Actions:")]
    [SerializeField] private InputActionAsset playerControls;
    [Header("Action Map")]
    [SerializeField] private string actionMapName = "Player";
    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string primaryFire = "PrimaryFire";
    
    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction jumpAction;
    private InputAction primaryFireAction;
    public Vector2 MovementInput;
    public Vector2 RotationInput;
    public bool JumpTriggered;
    public bool FirePrimaryTriggered;
    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        jumpAction = mapReference.FindAction(jump);
        primaryFireAction = mapReference.FindAction(primaryFire);
        SubscribeActionsToEvents();
    }
    private void SubscribeActionsToEvents()
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;
        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;
        jumpAction.performed += inputInfo => JumpTriggered = true;
        jumpAction.canceled += inputInfo => JumpTriggered = false;
        primaryFireAction.performed += inputInfo => FirePrimaryTriggered = true;
        primaryFireAction.canceled += inputInfo => FirePrimaryTriggered = false;
        
    }
    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }
    
    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }
}

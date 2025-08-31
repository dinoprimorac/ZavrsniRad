using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Actions:")]
    [SerializeField] private InputActionAsset playerControls = null;
    [Header("Action Map")]
    [SerializeField] private string actionMapName = "PlayerActionMap";
    [Header("Action Name References")]
    [SerializeField] private string movement = "Movement";
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string primaryFire = "PrimaryFire";
    [SerializeField] private string secondaryFire = "SecondaryFire";
    [SerializeField] private string weaponSwap = "WeaponSwap";
    [SerializeField] private string action = "Action";
    [SerializeField] private string pause = "Pause";
    
    private InputAction movementAction = null;
    private InputAction rotationAction = null;
    private InputAction jumpAction = null;
    private InputAction primaryFireAction = null;
    private InputAction secondaryFireAction = null;
    private InputAction weaponSwapAction = null;
    private InputAction interactAction = null;
    private InputAction pauseAction = null;
    
    public Vector2 MovementInput = Vector2.zero;
    public Vector2 RotationInput = Vector2.zero;
    public bool JumpTriggered = false;
    public bool FirePrimaryTriggered = false;
    public bool FireSecondaryTriggered = false;
    
    public string selectedSlot;
    public int weaponSwapTriggered = -1;

    public bool InteractionTriggered = false;
    public bool PauseTriggered = false;


    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        jumpAction = mapReference.FindAction(jump);
        primaryFireAction = mapReference.FindAction(primaryFire);
        secondaryFireAction = mapReference.FindAction(secondaryFire);
        weaponSwapAction = mapReference.FindAction(weaponSwap);
        interactAction = mapReference.FindAction(action);
        pauseAction = mapReference.FindAction(pause);
        SubscribeActionsToEvents();
    }

    private void SubscribeActionsToEvents()
    {
        movementAction.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
        movementAction.canceled += ctx => MovementInput = Vector2.zero;

        rotationAction.performed += ctx => RotationInput = ctx.ReadValue<Vector2>();
        rotationAction.canceled += ctx => RotationInput = Vector2.zero;

        jumpAction.performed += ctx => JumpTriggered = true;
        jumpAction.canceled += ctx => JumpTriggered = false;

        primaryFireAction.performed += ctx => FirePrimaryTriggered = true;
        primaryFireAction.canceled += ctx => FirePrimaryTriggered = false;

        secondaryFireAction.performed += ctx => FireSecondaryTriggered = true;
        secondaryFireAction.canceled += ctx => FireSecondaryTriggered = false;

        weaponSwapAction.started += ctx => weaponSwapTriggered = ctx.control.ToString()[ctx.control.ToString().Length - 1] - '1';
        weaponSwapAction.canceled += ctx => weaponSwapTriggered = -1;

        interactAction.started += ctx => InteractionTriggered = true;
        interactAction.canceled += ctx => InteractionTriggered = false;

        pauseAction.started += ctx => PauseTriggered = true;
        pauseAction.canceled += ctx => PauseTriggered = false;
        
    }

    private void LateUpdate()
    {
        weaponSwapTriggered = -1;
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

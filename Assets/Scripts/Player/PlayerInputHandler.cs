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
    //
    [SerializeField] private string weaponSwap = "WeaponSwap";

    private InputAction movementAction = null;
    private InputAction rotationAction = null;
    private InputAction jumpAction = null;
    private InputAction primaryFireAction = null;
    //
    private InputAction weaponSwapAction = null;

    public Vector2 MovementInput = Vector2.zero;
    public Vector2 RotationInput = Vector2.zero;
    public bool JumpTriggered = false;
    public bool FirePrimaryTriggered = false;

    public string selectedSlot;
    private char weaponSlotChar;
    public int weaponSlot = -1;

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        movementAction = mapReference.FindAction(movement);
        rotationAction = mapReference.FindAction(rotation);
        jumpAction = mapReference.FindAction(jump);
        primaryFireAction = mapReference.FindAction(primaryFire);
        weaponSwapAction = mapReference.FindAction(weaponSwap);
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

        // weaponSwapAction.started += ctx => selectedSlot = ctx.control.ToString();
        weaponSwapAction.started += ctx =>
        {
            weaponSlot = ctx.control.ToString()[ctx.control.ToString().Length-1] - '0';
        };
        weaponSwapAction.canceled += ctx => selectedSlot = "";

    }

    private void LateUpdate()
    {
        weaponSlot = -1;
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

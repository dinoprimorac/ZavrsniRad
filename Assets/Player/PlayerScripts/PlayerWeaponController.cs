using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(WeaponInventory))]
public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private WeaponInventory inventory;

    private Weapon current;

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        inventory = GetComponent<WeaponInventory>();
    }

    private void OnEnable()
    {
        if (inventory != null)
            inventory.OnEquippedChanged += HandleEquippedChanged;

        current = inventory?.Current();
    }

    private void OnDisable()
    {
        if (inventory != null)
            inventory.OnEquippedChanged -= HandleEquippedChanged;
    }

    private void Update()
    {
        HandleFireAction();
        HandleWeaponSwitch();
        
    }

    private void HandleFireAction()
    {
        var weapon = inventory.Current();
        if (weapon == null)
            return;

        if (input.FirePrimaryTriggered)
            weapon.TryShoot();
    }

    private void HandleWeaponSwitch()
    {
        int weaponSwapKey = input.weaponSwapTriggered;

        if (weaponSwapKey != -1)
            Debug.Log(weaponSwapKey);

        if (weaponSwapKey < 0 || weaponSwapKey == inventory.EquippedIndex)
        {
            return;
        }     
        inventory.SwapWeapon(weaponSwapKey);
    }

    private void HandleEquippedChanged(int slotIndex, GameObject go) {
        current = go ? go.GetComponent<Weapon>() : null;
    }
}

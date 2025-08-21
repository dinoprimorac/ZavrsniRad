using UnityEngine;

[RequireComponent(typeof(WeaponInventory))]
public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler input;
    [SerializeField] private Transform weaponHolder;
    private WeaponInventory inventory;

    private void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        inventory = GetComponent<WeaponInventory>();
    }

    private void Update()
    {
        HandleFire();
        HandleWeaponSwapping();
    }

    private void HandleFire()
    {
        if (input.FirePrimaryTriggered)
        {
            // pucaj sa trenutnim oružjem
        }
    }

    private void HandleWeaponSwapping()
    {
        if (input.weaponSwapTriggered > -1)
        {
            // prosljedi vrijednost tipke funkciji za promjenu oružja
        }
    }
}

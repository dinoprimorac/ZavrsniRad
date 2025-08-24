using System;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Camera playerCamera;

    [Header("Inventory State")]
    [SerializeField] private Weapon[] weaponInventory = new Weapon[6];
    [SerializeField] private int equippedIndex;

    [SerializeField] private Weapon currentWeapon;

    public event Action<int, GameObject> OnEquippedChanged;
    public event Action<int, int> OnAmmoChanged;

    public int EquippedIndex => equippedIndex;
    public Weapon Current() => currentWeapon;

    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        equippedIndex = -1;
    }

    public void CollectWeapon(WeaponDefinition wpDef)
    {
        int inventoryIndex = wpDef.assignedInventorySlot;

        if (weaponInventory[inventoryIndex] != null)
        {
            return;
        }

        GameObject wpObject = Instantiate(wpDef.prefab, weaponHolder);
        wpObject.GetComponent<BoxCollider>().enabled = false;
        wpObject.transform.localPosition = Vector3.zero;
        wpObject.transform.localRotation = Quaternion.identity;
        wpObject.SetActive(false);

        Weapon wpScript = wpObject.GetComponent<Weapon>();


        wpScript.ApplyWeaponData(wpDef);
        wpScript.Init(this, playerCamera);

        weaponInventory[inventoryIndex] = wpScript;

        SwapWeapon(inventoryIndex);
    }

    public void SwapWeapon(int swapIndex)
    {
        if (weaponInventory[swapIndex] == null)
            return;

        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        equippedIndex = swapIndex;
        currentWeapon = weaponInventory[swapIndex];
        currentWeapon.gameObject.SetActive(true);

        NotifyAmmoChanged(currentWeapon);
        OnEquippedChanged?.Invoke(equippedIndex, currentWeapon.gameObject);

    }

    public void NotifyAmmoChanged(Weapon weapon) {

        if (weapon == null) return;
        OnAmmoChanged?.Invoke(weapon.CurrentAmmmo, weapon.MaxAmmo);
    }
}

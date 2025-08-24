using System;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private Camera playerCamera;

    [Header("Inventory State")]
    [SerializeField] private Weapon[] weaponInventory = new Weapon[6];
    [SerializeField] private int equippedIndex;

    private Weapon currentWeapon;

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
        var inventoryIndex = wpDef.assignedInventorySlot;
        if (inventoryIndex == equippedIndex)
        {
            // moguće dodati logiku da se samo doda ammoa na to oružje
            return;
        }

        GameObject collectedWeapon = Instantiate(wpDef.prefab, weaponHolder);
        collectedWeapon.transform.localPosition = Vector3.zero;
        collectedWeapon.transform.localEulerAngles = Vector3.zero;
        collectedWeapon.SetActive(false);

        collectedWeapon.GetComponent<BoxCollider>().enabled = false;

        Weapon w = collectedWeapon.GetComponent<Weapon>();

        w.ApplyWeaponData(wpDef);
        w.Init(this, playerCamera);

        weaponInventory[inventoryIndex] = w;

        Debug.Log("Došlo je do kraja, ide swap!");

        SwapWeapon(inventoryIndex);

    }

    public void SwapWeapon(int swapIndex)
    {

        if (weaponInventory[swapIndex] == null)
        {
            return;
        }

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

using System;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public event Action<GameObject> OnEquippedChanged;
    public event Action<int, int> OnAmmoChanged;

    [SerializeField] private Transform wpHolder;
    [SerializeField] private Camera playerCam;

    private GameObject[] slots = new GameObject[6]; // polje koje sadrži pokupljena oružja
    private int equippedIndex = -1;

    public void AddWeapon(WeaponDefinition def)
    {

    }

    public void Equip(int index)
    {

    }

    public void AddAmmo(AmmoType type, int Amount)
    {

    }

    private void SubscribeCurrentWeapon()
    {

    }

    private void UnsubscribeCurrentWeapon()
    {

    }

    private void HandleAmmoChanged()
    {

    }

    public void NotifyAmmoChanged()
    {
        
    }
}

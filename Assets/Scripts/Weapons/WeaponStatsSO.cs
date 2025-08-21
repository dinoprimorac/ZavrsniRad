
using UnityEngine;
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon/WeaponData")]
public class WeaponStatsSO : ScriptableObject
{
    public string weaponName;
    public int maxAmmo;
    public float fireRate;
    public int damage;
    public float range;
    public int weaponInventoryIndex;
    public LayerMask targetLayerMask;
}

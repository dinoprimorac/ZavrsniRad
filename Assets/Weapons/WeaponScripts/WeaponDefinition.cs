using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "WeaponDataDefinition")]
public class WeaponDefinition : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public int assignedInventorySlot = 0;

    [Header("Weapon data for functionalities")]
    public string weaponName = "";
    public int maxAmmo = 100;
    public int startAmmo = 20;
    public float fireRate = 10f;
    public int weaponDamage = 10;
    public float weaponMaxRange = 200f;

    [Header("Visual/Audio effects")]
    public ParticleSystem muzzleFlash;
    public AudioClip shotSfx;

}

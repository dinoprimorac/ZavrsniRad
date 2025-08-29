using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDefinition wpData;

    [SerializeField] protected int currentAmmo;
    protected float nextFireTime;
    protected Camera playerCam;
    protected WeaponInventory inventory;

    public event Action<int, int> OnAmmoChanged;

    public int CurrentAmmmo => currentAmmo;
    public int MaxAmmo => wpData ? wpData.maxAmmo : 0;

    public WeaponDefinition WeaponData => wpData;
    public void ApplyWeaponData(WeaponDefinition d) { wpData = d; }

    protected virtual void Awake()
    {
        currentAmmo = wpData.startAmmo;
    }

    public void Init(WeaponInventory inv, Camera cam)
    {
        inventory = inv;
        playerCam = cam;

        if (currentAmmo < 0)
        {
            currentAmmo = Mathf.Clamp(wpData.startAmmo, 0, wpData.maxAmmo);
            OnAmmoChanged?.Invoke(currentAmmo, MaxAmmo);
        }
    }

    public void TryShoot()
    {
        if (Time.time < nextFireTime) return;
        if (currentAmmo <= 0) return;

        nextFireTime = Time.time + wpData.fireRate;
        currentAmmo--;
        inventory?.NotifyAmmoChanged(this);

        HandleShoot();
    }

    protected abstract void HandleShoot();

    protected Vector3 ApplySpread(Vector3 baseDirection, float spreadDegrees)
    {
        if (spreadDegrees <= 0f) return baseDirection;

        Vector2 spread = UnityEngine.Random.insideUnitCircle * spreadDegrees;

        Vector3 right = playerCam.transform.right;
        Vector3 up = playerCam.transform.up;

        Vector3 spreadDirection = baseDirection + spread.x * right + spread.y * up;
        return spreadDirection.normalized;
    }
}

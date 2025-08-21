using UnityEngine;
public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] public WeaponStatsSO weaponStats;
    public int currentAmmo { get; private set; }
    private float nextTimeToFire = 0f;

    [SerializeField] protected Animator weaponAnimation;
    [SerializeField] public Camera playerCamera;
    
    private void Awake()
    {
        currentAmmo = 0;
        // currentAmmo = weaponStats.maxAmmo;
        playerCamera = GetComponentInParent<Camera>();
        weaponAnimation = GetComponent<Animator>();
    }

    public void TryShoot()
    {
        if (currentAmmo <= 0)
        {
            UIManager.Instance.AlertPlayer("Out of Ammo!");
            return;
        }
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + weaponStats.fireRate;
            HandleShoot();
        }
    }

    private void HandleShoot()
    {
        currentAmmo--;
        UIManager.Instance.UpdateAmmo(currentAmmo);
        Debug.Log(weaponStats.name + " shot! Bullets left: " + currentAmmo);
        Shoot();
    }

    protected abstract void Shoot();

}

using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public WeaponStatsSO weaponStats;
    [SerializeField] public Camera playerCamera;
    private int currentAmmo = 0;
    private float nextTimeToFire = 0f;
    private Animator weaponAnimation;

    private void Awake()
    {
        currentAmmo = weaponStats.maxAmmo;
        playerCamera = GetComponentInParent<Camera>();
        weaponAnimation = GetComponent<Animator>();
        // UIManager.Instance.UpdateAmmo(currentAmmo);
    }
    
    public void TryShoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log(weaponStats.name + " is out of ammo!");
            return;
        }
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + weaponStats.fireRate;
            HandleShoot();
            weaponAnimation.SetTrigger("Fire");
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

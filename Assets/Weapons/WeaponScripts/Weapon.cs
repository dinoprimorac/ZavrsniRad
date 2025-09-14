using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Config")]
    [SerializeField] protected WeaponDefinition wpData;

    [Header("Weapon Audio")]
    [SerializeField] protected AudioSource sfx;
    [SerializeField] protected AudioClip shootSfx;

    [Header("Raycast Mask")]
    [Tooltip("Layers bullets can hit (Sensors will always be excluded at runtime).")]
    [SerializeField] protected LayerMask hitMask = ~0;
    [Tooltip("Layer name for your sensor triggers (Aggro/Attack).")]
    [SerializeField] protected string sensorsLayerName = "Sensors";

    protected int currentAmmo;
    protected float nextFireTime;

    protected Camera playerCam;
    protected WeaponInventory inventory;

    public event Action<int, int> OnAmmoChanged;

    public int CurrentAmmmo => currentAmmo;
    public int MaxAmmo => wpData ? wpData.maxAmmo : 0;

    private int _sensorsMask;
    protected int RaycastMask => hitMask & ~_sensorsMask;

    public void ApplyWeaponData(WeaponDefinition d) { wpData = d; }

    protected virtual void Awake()
    {
        if (!sfx) sfx = GetComponent<AudioSource>() ?? GetComponentInChildren<AudioSource>(true);
        currentAmmo = wpData.startAmmo;
        shootSfx = wpData.shotSfx;

        _sensorsMask = LayerMask.GetMask(sensorsLayerName);
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

        float t = Mathf.Tan(spreadDegrees * Mathf.Deg2Rad);
        Vector2 offset = UnityEngine.Random.insideUnitCircle * t;

        Vector3 right = playerCam.transform.right;
        Vector3 up    = playerCam.transform.up;

        Vector3 dir = (baseDirection + offset.x * right + offset.y * up).normalized;
        return dir;
    }

    protected void PlayAudio()
    {
        if (!sfx || !shootSfx) return;
        sfx.pitch = UnityEngine.Random.Range(0.96f, 1.04f);
        sfx.PlayOneShot(shootSfx, 1f);
    }

    protected void DamageEnemy(int amount, IDamageable damageableComponent)
    {
        if (damageableComponent != null)
            damageableComponent.TakeDamage(amount);
    }
}

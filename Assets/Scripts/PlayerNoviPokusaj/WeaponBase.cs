using System;
[SerializeField] protected WeaponDefinition definition;


protected WeaponInventory inventory;
protected Camera playerCam;


protected float nextFireTime = 0f;
[SerializeField] protected int currentMag = -1;


public event Action<int,int> OnMagChanged;


public AmmoType AmmoType => definition.ammoType;
public int CurrentMag => currentMag;
public int MagSize => definition.magazineSize;


protected virtual void Awake() {
if (definition == null) {
Debug.LogError($"{name}: WeaponDefinition not assigned.");
}
}


public virtual void Init(WeaponInventory inventory, Camera playerCam) {
this.inventory = inventory;
this.playerCam = playerCam;
if (currentMag < 0) {
currentMag = Mathf.Clamp(definition.startingMagazine, 0, definition.magazineSize);
OnMagChanged?.Invoke(currentMag, definition.magazineSize);
}
}


public virtual void TryShoot() {
if (Time.time < nextFireTime) return;
if (currentMag <= 0) {
return; // out of ammo
}


// Fire
currentMag--;
OnMagChanged?.Invoke(currentMag, definition.magazineSize);
inventory?.NotifyAmmoChanged(this);


// Simple raycast hit scan (placeholder)
if (playerCam) {
Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
if (Physics.Raycast(ray, out RaycastHit hit, definition.maxRange, ~0, QueryTriggerInteraction.Ignore)) {
// TODO: Apply damage to hit target via your health system
Debug.DrawLine(ray.origin, hit.point, Color.yellow, 0.2f);
}
}


// FX (optional)
if (definition.muzzleFlash) definition.muzzleFlash.Play();
var audio = GetComponent<AudioSource>();
if (definition.shotSfx && audio) audio.PlayOneShot(definition.shotSfx);


// Rate of fire
nextFireTime = Time.time + (1f / Mathf.Max(0.0001f, definition.fireRate));
}
}

using UnityEngine;
public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] public GameObject equippedWeapon = null;
    [SerializeField] private GameObject[] weaponsInventory = new GameObject[6];

    private Weapon equippedWeaponScript;
    private PlayerInputHandler playerInputHandler;

    [SerializeField] private Transform weaponPlace;

    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        HandleWeaponShooting();
        HandleWeaponSwapping();
    }

    private void HandleWeaponShooting()
    {
        if (playerInputHandler.FirePrimaryTriggered && equippedWeapon)
        {
            equippedWeaponScript = equippedWeapon.GetComponent<Weapon>();
            equippedWeaponScript.TryShoot();
        }
        else if (playerInputHandler.FirePrimaryTriggered && !equippedWeapon)
        {
            Debug.Log("You don't have a weapon!");
        }
    }

    private void HandleWeaponSwapping()
    {
        if (playerInputHandler.weaponSwapTriggered > -1)
        {
            SwitchWeapon(playerInputHandler.weaponSwapTriggered);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            AddWeapon(other.gameObject);

            Weapon newWeapon = other.GetComponent<Weapon>();
            newWeapon.enabled = true;
            UIManager.Instance.UpdateAmmo(newWeapon.currentAmmo);
        }
    }
    
    private void AddWeapon(GameObject pickupWeapon)
    {

        int indexInventory = pickupWeapon.GetComponent<Weapon>().weaponStats.weaponInventoryIndex - 1;
        weaponsInventory[indexInventory] = Instantiate(pickupWeapon);
        Destroy(pickupWeapon);
        DisableCurrentWeapon();
        equippedWeapon = weaponsInventory[indexInventory];
        equippedWeapon.GetComponent<Rotation>().enabled = false;
        equippedWeapon.GetComponent<BoxCollider>().enabled = false;
        PutWeaponInHolder();
        equippedWeaponScript = equippedWeapon.GetComponent<Weapon>();
    }
    private void PutWeaponInHolder()
    {
        equippedWeapon.GetComponent<Weapon>().playerCamera = GetComponentInChildren<Camera>();
        equippedWeapon.transform.SetParent(weaponPlace.transform);
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localEulerAngles = Vector3.zero;
    }
    private void DisableCurrentWeapon()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.gameObject.SetActive(false);
        }
        
    }
    
    private void EnableWeapon()
    {

    }

    private void SwitchWeapon(int inventoryIndex)
    {
        if (weaponsInventory[inventoryIndex] == null) return;
        DisableCurrentWeapon();
        equippedWeapon = weaponsInventory[inventoryIndex];
        equippedWeapon.SetActive(true);
        equippedWeaponScript = equippedWeapon.GetComponent<Weapon>();
    }
    private void AddAmmo()
    {
        // igrač dodaje ammo ovisno o tipu amma za oružje
    }
}

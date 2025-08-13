
using UnityEngine;
public class WeaponManager : MonoBehaviour
{
    [SerializeField] public GameObject equippedWeapon = null;
    [SerializeField] private GameObject[] weaponsInventory = new GameObject[5];
    private Weapon equippedWeaponScript;
    private PlayerInputHandler playerInputHandler;
    [SerializeField] private Transform weaponPlace;
    public void Awake()
    {
        playerInputHandler = GetComponentInParent<PlayerInputHandler>();
    }
    public void Update()
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            AddWeapon(other.gameObject);
            Debug.Log("Igrač je pokupio oružje!");
        }
    }
    private void AddWeapon(GameObject pickupWeapon)
    {
        
        int indexInventory = pickupWeapon.GetComponent<Weapon>().weaponStats.weaponInventoryIndex;
        weaponsInventory[indexInventory] = Instantiate(pickupWeapon);
        Destroy(pickupWeapon);
        DisableCurrentWeapon();
        equippedWeapon = weaponsInventory[indexInventory];
        // equippedWeapon.GetComponent<Rotation>().enabled = false;
        equippedWeapon.GetComponent<BoxCollider>().enabled = false;
        PutWeaponInHolder();
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
        // igrač preko brojeva na tipkovnici bira koje oružje želi trenutno imati
    }
    private void AddAmmo()
    {
        // igrač dodaje ammo ovisno o tipu amma za oružje
    }
}

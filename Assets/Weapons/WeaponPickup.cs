using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour
{
    [Header("Weapon Definition")]
    [SerializeField] private WeaponDefinition weaponDef;

    private void Awake()
    {
        // weaponDef = GetComponent<WeaponDefinition>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Provjeri da li je igrač
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<WeaponInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("Player nema WeaponInventory komponentu!");
            return;
        }

        inventory.CollectWeapon(weaponDef);

        Destroy(gameObject);
    }
}

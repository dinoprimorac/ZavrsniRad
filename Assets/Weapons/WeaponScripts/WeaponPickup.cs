using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour
{
    [Header("Weapon Definition")]
    [SerializeField] private WeaponDefinition weaponDef;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<WeaponInventory>();
        if (inventory == null)
        {
            Debug.LogWarning("Player nema WeaponInventory komponentu!");
            return;
        }

        Debug.Log("Došao do collect funkcije");
        inventory.CollectWeapon(weaponDef);
        Destroy(gameObject);
    }
}

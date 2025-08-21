using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WeaponPickup : MonoBehaviour {
    [SerializeField] private WeaponDefinition definition;

    private void Reset() {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
        gameObject.tag = "Pickup"; // optional
    }


    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out WeaponInventory inv)) {
            inv.AddWeapon(definition);
            Destroy(gameObject);
        }
    }
}
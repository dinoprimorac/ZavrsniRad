using UnityEngine;

public interface IDamageable { void TakeDamage(int amount); }

public class AttackHitbox : MonoBehaviour
{
    public string playerTag = "Player";
    public int damage = 10;

    bool canDamage;
    bool alreadyHit;

    public void BeginWindow()
    {
        canDamage = true;
        alreadyHit = false;
        gameObject.SetActive(true); // ensure enabled during lunge
    }

    public void EndWindow()
    {
        canDamage = false;
        gameObject.SetActive(true); // you can keep it active; damage is gated by canDamage
    }

    void OnTriggerEnter(Collider other)
    {
        if (!canDamage || alreadyHit) return;
        if (!other.CompareTag(playerTag)) return;

        // Try interface first
        var dmg = other.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
            alreadyHit = true;
            return;
        }

        // Fallback: look for a common "Health" script
        var health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            alreadyHit = true;
        }
    }
}

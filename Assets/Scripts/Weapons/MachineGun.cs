using UnityEngine;

public class MachineGun : Weapon
{
    protected override void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 spreadDirection = ApplySpread(ray.direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, weaponStats.range))
        {
            Debug.DrawRay(ray.origin, spreadDirection * hit.distance, Color.green, 200f);
            Debug.Log("Hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Enemy"))
            {
                DamageEnemy(hit);
            }
        }
    }

    private Vector3 ApplySpread(Vector3 direction)
    {
        float spreadAmount = 0.05f; 
        Vector2 spread = Random.insideUnitCircle * spreadAmount;
        Vector3 right = playerCamera.transform.right;
        Vector3 up = playerCamera.transform.up;
        Vector3 spreadDirection = direction + spread.x * right + spread.y * up;
        return spreadDirection.normalized;
    }

}

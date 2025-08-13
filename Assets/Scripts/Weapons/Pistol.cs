using UnityEngine;

public class Pistol : Weapon
{

    protected override void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponStats.range))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue, 1f);
            Debug.Log("Hit: " + hit.collider.name);
            
            if (hit.collider.CompareTag("Enemy"))
            {
                DamageEnemy(hit);
            }
        }
    }
}

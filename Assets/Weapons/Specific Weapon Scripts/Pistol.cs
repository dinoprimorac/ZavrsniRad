using UnityEngine;

public class Pistol : Weapon
{
    protected override void HandleShoot()
    {
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, WeaponData.weaponMaxRange))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.blue, 1f);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * wpData.weaponMaxRange, Color.blue, 1f);

        }
    }
}

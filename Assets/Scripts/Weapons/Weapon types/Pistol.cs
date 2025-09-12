using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private string sensorsLayerName = "Sensors";
    [SerializeField] private ProceduralRecoil recoil;

    protected override void HandleShoot()
    {
        recoil?.FireKick();
        Vector3 origin = playerCam.transform.position;
        Vector3 dir = playerCam.transform.forward;

        // Mask = everything EXCEPT Sensors
        int mask = ~LayerMask.GetMask(sensorsLayerName);

        if (Physics.Raycast(origin, dir, out RaycastHit hit, wpData.weaponMaxRange, mask))
        {

            var damageable = hit.collider.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(wpData.weaponDamage);
            }

            Debug.DrawLine(origin, hit.point, Color.red, 0.2f);
        }
        else
        {
            Debug.DrawRay(origin, dir * wpData.weaponMaxRange, Color.blue, 0.5f);
        }
    }
}


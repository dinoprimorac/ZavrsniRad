using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Shotgun")]
    [SerializeField] private int pellets = 10;
    [SerializeField] private float spreadDegrees = 8f;
    [SerializeField] private ProceduralRecoil recoil;

    protected override void HandleShoot()
    {
        recoil?.FireKick();
        PlayAudio();

        Vector3 origin  = playerCam.transform.position;
        Vector3 forward = playerCam.transform.forward;

        var hitCounts = new Dictionary<IDamageable, int>();

        int mask = ~LayerMask.GetMask(sensorsLayerName);

        for (int i = 0; i < pellets; i++)
        {
            Vector3 dir = ApplySpread(forward, spreadDegrees);

            if (Physics.Raycast(origin, dir, out RaycastHit hit, wpData.weaponMaxRange, mask, QueryTriggerInteraction.Ignore))
            {
                var damageable = hit.collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    if (hitCounts.TryGetValue(damageable, out int count))
                        hitCounts[damageable] = count + 1;
                    else
                        hitCounts[damageable] = 1;
                }
            }
        }

        foreach (var kvp in hitCounts)
        {
            int totalDamage = kvp.Value * wpData.weaponDamage;
            DamageEnemy(totalDamage, kvp.Key);
        }
    }
}

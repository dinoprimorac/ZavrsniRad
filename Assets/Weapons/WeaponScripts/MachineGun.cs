using UnityEngine;

public class MachineGun : Weapon
{
    [Header("Machine Gun")]
    [SerializeField] private float spreadDegrees = 1.2f;
    [SerializeField] private ProceduralRecoil recoil;

    protected override void HandleShoot()
    {
        recoil?.FireKick();
        PlayAudio();

        Vector3 origin = playerCam.transform.position;
        Vector3 baseDir = playerCam.transform.forward;
        Vector3 dir = ApplySpread(baseDir, spreadDegrees);

        if (Physics.Raycast(origin, dir, out RaycastHit hit, wpData.weaponMaxRange, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            var dmg = hit.collider.GetComponentInParent<IDamageable>();
            DamageEnemy(wpData.weaponDamage, dmg);
        }
    }
}

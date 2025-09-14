using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private ProceduralRecoil recoil;

    protected override void HandleShoot()
    {
        recoil?.FireKick();
        PlayAudio();

        Vector3 origin = playerCam.transform.position;
        Vector3 dir    = playerCam.transform.forward;

        if (Physics.Raycast(origin, dir, out RaycastHit hit, wpData.weaponMaxRange, RaycastMask, QueryTriggerInteraction.Ignore))
        {
            var dmg = hit.collider.GetComponentInParent<IDamageable>();
            DamageEnemy(wpData.weaponDamage, dmg);
        }
    }
}

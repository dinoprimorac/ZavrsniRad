using UnityEngine;
using System;

public class Shotgun : Weapon
{
    [Header("Shotgun")]
    [SerializeField] private int pellets = 10;
    [SerializeField] private float spreadDegrees = 15f;

    protected override void HandleShoot()
    {
        Ray centerRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        Vector3 origin = centerRay.origin;
        Vector3 forward = centerRay.direction;

        int pelletsHit = 0;

        for (int i = 0; i < pellets; i++)
        {
            Vector3 dir = ApplySpread(forward, spreadDegrees);

            if (Physics.Raycast(origin, dir, out RaycastHit hit))
            {
                Debug.DrawLine(origin, hit.point, Color.green, 2.0f);
                pelletsHit++;

            }
            else
            {
                Debug.DrawLine(origin, origin + dir * wpData.weaponMaxRange, Color.red, 2.0f);
            }
        }
        Debug.Log("Pellets hit: " + pelletsHit);
    }
}

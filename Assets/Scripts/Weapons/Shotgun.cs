
using System.Globalization;
using UnityEngine;
public class Shotgun : Weapon
{
    protected override void Shoot()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;
        int numberOfPalletsHit = 0;
        
        for (int i = 0; i < 10; i++)
        {
            Vector3 spreadDirection = ApplySpread(ray.direction);
            if (Physics.Raycast(ray.origin, spreadDirection, out hit, weaponStats.range))
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.DrawLine(ray.origin, ray.origin + spreadDirection * hit.distance, Color.green, 3f);
                    numberOfPalletsHit++;
                    DamageEnemy(hit);
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + spreadDirection * hit.distance, Color.red, 3f);
                }
            }
        }
       
       
    }
        
    private Vector3 ApplySpread(Vector3 direction){

        float spreadAmount = 0.3f; 
        Vector2 spread = Random.insideUnitCircle * spreadAmount;

        Vector3 right = playerCamera.transform.right;
        Vector3 up = playerCamera.transform.up;
        Vector3 spreadDirection = direction + spread.x * right + spread.y * up;
        
        return spreadDirection.normalized;
    }
}

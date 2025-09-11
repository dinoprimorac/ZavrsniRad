using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "EnemyDataDefinition")]
public class EnemyConfig : ScriptableObject
{
    public string enemyName = "";
    public int maxHealth = 30;
    public int damage = 10;
    public float movementSpeed = 2.0f;

}

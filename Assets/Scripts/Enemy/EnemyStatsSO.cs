
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy/EnemyData")]
public class EnemyStatsSO : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int attackDamage;
}

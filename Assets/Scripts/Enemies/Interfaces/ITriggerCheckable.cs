using UnityEngine;

public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsWithinAttackDistance { get; set; }
    void SetAggroStatus(bool isAggroed);
    void SetAttackDistanceBool(bool isWithinAttackDistance);
}

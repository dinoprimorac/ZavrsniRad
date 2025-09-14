using UnityEngine;

public class AnimatorEventRelay : MonoBehaviour
{
    [SerializeField] private EnemyBase enemy;
    private void Reset() { enemy = GetComponentInParent<EnemyBase>(); }

    public void AnimationEvent_Attack()    { enemy?.AnimationEvent_Attack(); }
    public void AnimationEvent_AttackEnd() { enemy?.AnimationEvent_AttackEnd(); }
}

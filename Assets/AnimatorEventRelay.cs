using UnityEngine;

public class AnimatorEventRelay : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private void Reset() { enemy = GetComponentInParent<Enemy>(); }

    public void AnimationEvent_Attack()    { enemy?.AnimationEvent_Attack(); }
    public void AnimationEvent_AttackEnd() { enemy?.AnimationEvent_AttackEnd(); }
}

using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator playerAnimation;
    [SerializeField] private CharacterController cc;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (cc.velocity.magnitude > 0)
        {
            playerAnimation.SetBool("WeaponSwayActive", true);
        }
        else
        {
            playerAnimation.SetBool("WeaponSwayActive", false);
        }
    }
}

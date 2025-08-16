using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private Animator weaponAnimation;

    private void Awake()
    {
        weaponAnimation = GetComponent<Animator>();
    }

    public void PlayWeaponAnimation()
    {
        weaponAnimation.SetTrigger("LoadShotgun");
    }



}

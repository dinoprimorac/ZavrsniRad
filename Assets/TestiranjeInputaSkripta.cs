using UnityEngine;

public class TestiranjeInputaSkripta : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler playerInputHandler;

    void Start()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    void Update()
    {
        Debug.Log(playerInputHandler.weaponSlot);
    }
}

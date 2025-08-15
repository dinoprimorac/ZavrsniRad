
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI ammo;
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void UpdateHealth(int healthValue)
    {
        health.text = healthValue.ToString();
    }

    public void UpdateAmmo(int ammoValue)
    {
        ammo.text = ammoValue.ToString();
    }
    
    
}

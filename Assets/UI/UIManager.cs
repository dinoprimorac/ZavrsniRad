using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    private void OnEnable()
    {
        PlayerHealth.HealthChanged += UpdateHealthUI;
        PlayerHealth.Died += ShowGameOver;
    }

    private void OnDisable()
    {
        PlayerHealth.HealthChanged -= UpdateHealthUI;
        PlayerHealth.Died -= ShowGameOver;
    }

    private void UpdateHealthUI(int current)
    {
        healthText.text = $"{current}";
    }

    private void ShowGameOver()
    {
        Debug.Log("Game Over!");
    }
}

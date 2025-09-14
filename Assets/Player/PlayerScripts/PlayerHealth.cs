using System;
using System.Collections;                   
using UnityEngine;
using UnityEngine.SceneManagement;           

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int MaxHealth { get; set; } = 100;
    public int CurrentHealth { get; set; }
    public bool IsDead { get; private set; }

    [Header("Death/Restart")]
    [SerializeField] private float restartDelay = 1.0f; 

    public static event Action<int> HealthChanged;
    public static event Action Died;

    private bool _isReloading;

    private void Awake()
    {
        CurrentHealth = 50;
        IsDead = false;
        _isReloading = false;
    }

    private void Start()
    {
        HealthChanged?.Invoke(CurrentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth - amount, 0, MaxHealth);
        HealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, MaxHealth);
        HealthChanged?.Invoke(CurrentHealth);
        Debug.Log("Player healed for " + amount);
    }

    public void Die()
    {
        if (IsDead) return;

        IsDead = true;
        Debug.Log("Player Died!");
        Died?.Invoke(); 

        if (!_isReloading)
            StartCoroutine(ReloadSceneAfterDelay());
    }

    private IEnumerator ReloadSceneAfterDelay()
    {
        _isReloading = true;

        if (Time.timeScale == 0f) Time.timeScale = 1f;

        if (restartDelay > 0f)
            yield return new WaitForSecondsRealtime(restartDelay);

        var active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex);
    }
}

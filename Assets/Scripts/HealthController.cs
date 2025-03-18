using UnityEngine;
using UnityEngine.Audio;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 6;  
    private int currentHealth;

    public Animator animator;  
    public string healthStateParameter = "AstronautHealthState";

    public AudioSource decSource;
    public AudioClip decCollisionSound;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthState();

        decSource = GetComponent<AudioSource>();
    }
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;  
        UpdateHealthState();
        if (decSource != null && decCollisionSound != null)
        {
            decSource.PlayOneShot(decCollisionSound);
        }
    }
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;  
        UpdateHealthState();
    }
    private void UpdateHealthState()
    {   
        animator.SetInteger(healthStateParameter, currentHealth);  
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

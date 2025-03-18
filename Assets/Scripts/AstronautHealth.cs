using UnityEngine;
using UnityEngine.Audio;

public class AstronautHealth : MonoBehaviour
{
    private HealthController healthController;
    private ScoreManager scoreManager;
    private MissionControl missionControl;
    private int currentHealth;

    public AudioSource flowerSource;
    public AudioClip flowerCollisionSound;

    public AudioSource dangerSource;
    public AudioClip dangerCollisionSound;

    public AudioSource randSource;
    public AudioClip randCollisionSound;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
        missionControl = Object.FindFirstObjectByType<MissionControl>();

        if (missionControl == null)
        {
            Debug.LogError("MissionControl reference is NULL in AstronautHealth!");
        }

        if (flowerSource == null)
        {
            flowerSource = gameObject.AddComponent<AudioSource>();
        }

        if (dangerSource == null)
        {
            dangerSource = gameObject.AddComponent<AudioSource>();
        }

        if (randSource == null)
        {
            randSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            healthController.DecreaseHealth(1);
            currentHealth = healthController.GetCurrentHealth();
            if (dangerSource != null && dangerCollisionSound != null)
            {
                dangerSource.PlayOneShot(dangerCollisionSound);
            }
            if (currentHealth == 0)
            {
                missionControl.GameOver(); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Spike") || collision.collider.CompareTag("Cactus"))
        {
            healthController.DecreaseHealth(1);
            currentHealth = healthController.GetCurrentHealth();
            if (dangerSource != null && dangerCollisionSound != null)
            {
                dangerSource.PlayOneShot(dangerCollisionSound);
            }
            if (currentHealth == 0)
            {
                missionControl.GameOver();
            }
        }

        if (collision.collider.CompareTag("Flower"))
        {
            if (healthController.GetCurrentHealth() < healthController.maxHealth)  
            {
                Debug.Log("Collided with flower");
                healthController.IncreaseHealth(1);
                Destroy(collision.gameObject);
                if (flowerSource != null && flowerCollisionSound != null)
                {
                    flowerSource.PlayOneShot(flowerCollisionSound);
                }
            }
        }

        if (collision.collider.CompareTag("Random Object"))
        {
            if (randSource != null && randCollisionSound != null)
            {
                randSource.PlayOneShot(randCollisionSound);
            }
        }
    }
}

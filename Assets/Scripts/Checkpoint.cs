using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isTriggered = false;
    private AudioSource audioSource;
    public AudioClip checkpoint;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (!isTriggered)
            {
                HealthController healthController = other.GetComponent<HealthController>();

                if (healthController != null)
                {
                    healthController.IncreaseHealth(healthController.maxHealth);
                }

                if (audioSource != null && checkpoint != null)
                {
                    audioSource.PlayOneShot(checkpoint);
                }

                isTriggered = true;
            }
        }
    }

    public void ResetCheckpoint()
    {
        isTriggered = false;
    }
}

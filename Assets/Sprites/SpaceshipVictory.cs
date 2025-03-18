using UnityEngine;

public class SpaceshipVictory : MonoBehaviour
{
    public AudioClip victorySound;
    private AudioSource audioSource;
    private MissionControl missionControl;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        missionControl = Object.FindFirstObjectByType<MissionControl>(); 
                                                                                          

        if (missionControl == null)
        {
            Debug.LogError("MissionControl reference is NULL in SpaceshipVictory!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collided with spaceship!");
            if (audioSource != null && victorySound != null)
            {
                audioSource.PlayOneShot(victorySound);
            }
            missionControl.GameComplete();
        }
    }
}

using UnityEngine;

public class AstronautAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;
    private AudioSource audioSource; 
    public AudioClip moveSound; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            animator.Play("Move_Astronaut");
            if (!audioSource.isPlaying)
            {
                audioSource.clip = moveSound; 
                audioSource.Play(); 
            }
        }
        else
        {
            animator.Play("Idle_Astronaut");
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    public void Die()
    {
        isDead = true;
        animator.Play("Death_Astronaut");
    }
}

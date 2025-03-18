using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    private AudioSource audioSource;
    public AudioClip shootSound;

    public AudioSource bulletAudioSource;

    void Start()
    {
        if (bulletAudioSource != null)
        {
            audioSource = bulletAudioSource;
        }
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (audioSource != null && shootSound != null)
        {
            Debug.Log("Playing shoot sound");
            audioSource.PlayOneShot(shootSound);
        }

        Vector3 spawnPosition = transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; 
        Vector2 direction = (mousePos - spawnPosition).normalized;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
    }
}

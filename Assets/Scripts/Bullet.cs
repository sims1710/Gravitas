using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 2f;
    public float restoreDelay = 15f;

    private ScoreManager scoreManager;

    void Start()
    {
        Destroy(gameObject, lifetime);
        scoreManager = Object.FindFirstObjectByType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            HealthController healthController = other.GetComponent<HealthController>();

            if (healthController != null)
            {
                healthController.DecreaseHealth(damage);
                Destroy(gameObject);
            }

            if (healthController.GetCurrentHealth() <= 0)
            {
                Destroy(other.gameObject);

                if (other.CompareTag("Enemy"))
                {
                    scoreManager.OnEnemyKilled();
                }
                else if (other.CompareTag("Boss"))
                {
                    scoreManager.OnBossKilled();
                }

                StartCoroutine(RestoreEnemyHealth(other.gameObject, healthController));
            }
        }
    }

    private IEnumerator RestoreEnemyHealth(GameObject enemy, HealthController healthController)
    {
        int restoreCount = 0;
        while (restoreCount < 4) 
        {
            yield return new WaitForSeconds(restoreDelay);

            if (enemy != null)
            {
                healthController.IncreaseHealth(healthController.maxHealth);
            }

            restoreCount++; 
        }
    }

}

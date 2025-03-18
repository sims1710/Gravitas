using UnityEngine;

public class NoInteraction : MonoBehaviour
{
    // This script ensures no interaction occurs when colliding with the player
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return;
        }
    }
}


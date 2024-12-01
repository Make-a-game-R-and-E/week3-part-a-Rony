using UnityEngine;

public class PlayerProtection : MonoBehaviour
{
    private bool isShieldActive = false;

    public void ActivateShield(float duration)
    {
        if (isShieldActive) return; // If the shield is already active, do nothing
        isShieldActive = true;

        // Turning off the player's collider to prevent damage
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // We will make sure that the protection is canceled after the time we set
        Invoke(nameof(DeactivateShield), duration);
        Debug.Log("Shield activated! Player is protected.");
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        Debug.Log("Shield deactivated! Player is no longer protected.");

        // Restarting the collider after the shield has expired
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isShieldActive && other.CompareTag("Enemy"))
        {
            // Ignoring the enemy's damage while the shield is active
            Debug.Log("Enemy collision ignored due to active shield.");
            return;
        }

        // Normal handling of contacts (when the shield is not active)
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by enemy!");
        }
    }
}


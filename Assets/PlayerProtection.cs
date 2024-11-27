using UnityEngine;

public class PlayerProtection : MonoBehaviour
{
    private bool isShieldActive = false;

    public void ActivateShield(float duration)
    {
        if (isShieldActive) return; // אם כבר המגן פעיל, לא לעשות כלום
        isShieldActive = true;

        // כיבוי ה-Collider של השחקן כדי למנוע פגיעות
        Collider2D playerCollider = GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // נוודא שההגנה תתבטל אחרי הזמן שקבענו
        Invoke(nameof(DeactivateShield), duration);
        Debug.Log("Shield activated! Player is protected.");
    }

    public void DeactivateShield()
    {
        isShieldActive = false;
        Debug.Log("Shield deactivated! Player is no longer protected.");

        // הפעלת ה-Collider מחדש לאחר שהמגן פג
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
            // התעלמות מהפגיעה של האויב בזמן שהמגן פעיל
            Debug.Log("Enemy collision ignored due to active shield.");
            return;
        }

        // טיפול רגיל במגעים (כאשר המגן אינו פעיל)
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by enemy!");
            // כאן ניתן להוסיף את הקוד לפסילת השחקן
        }
    }
}


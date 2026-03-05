using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthFill;   // The foreground health sprite

    private float maxWidth;
    private Vector3 startPosition;

    void Start()
    {
        maxWidth = healthFill.localScale.x;
        startPosition = healthFill.localPosition;
    }

    public void SetHealth(float newHealth, float maxHealth)
    {
        float fillPercent = Mathf.Clamp(newHealth, 0, maxHealth) / maxHealth;
        float newWidth = maxWidth * fillPercent;

        // Shrink the scale
        healthFill.localScale = new Vector3(newWidth, healthFill.localScale.y, 1);

        // Shift left to compensate so the left edge stays anchored
        float offset = (maxWidth - newWidth) / 2f;
        healthFill.localPosition = new Vector3(startPosition.x - offset, startPosition.y, startPosition.z);
    }
}
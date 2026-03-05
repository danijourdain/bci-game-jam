using UnityEngine;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Transform fillBar;   // The foreground sprite

    private float maxWidth;
    private Vector3 startPosition;

    void Start()
    {
        maxWidth = fillBar.localScale.x;
        startPosition = fillBar.localPosition;
    }

    public void SetHealth(float newFill, float maxFill)
    {
        float fillPercent = Mathf.Clamp(newFill, 0, maxFill) / maxFill;
        float newWidth = maxWidth * fillPercent;

        // Shrink the scale
        fillBar.localScale = new Vector3(newWidth, fillBar.localScale.y, 1);

        // Shift left to compensate so the left edge stays anchored
        float offset = (maxWidth - newWidth) / 2f;
        fillBar.localPosition = new Vector3(startPosition.x - offset, startPosition.y, startPosition.z);
    }
}
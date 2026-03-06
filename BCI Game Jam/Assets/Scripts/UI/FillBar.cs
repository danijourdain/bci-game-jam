using UnityEngine;

public class FillBar : MonoBehaviour
{
    [SerializeField] private Transform fillBar;   // The foreground sprite

    private readonly float maxWidth = 0.97f;
    [SerializeField] private float anchorX = 0f; 
    private Vector3 startPosition;

    void Start()
    {
        startPosition = fillBar.localPosition;
    }

    public void SetFill(float newFill, float maxFill)
    {
        float fillPercent = Mathf.Clamp(newFill, 0, maxFill) / maxFill;
        float newWidth = maxWidth * fillPercent;

        // Shrink the scale
        fillBar.localScale = new Vector3(newWidth, fillBar.localScale.y, 1);

        fillBar.localPosition = new Vector3(anchorX - maxWidth / 2f + newWidth / 2f, fillBar.localPosition.y, fillBar.localPosition.z);

    }
}
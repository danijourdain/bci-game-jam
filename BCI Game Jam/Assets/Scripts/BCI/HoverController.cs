using UnityEngine;
using Cognixion.Services;

public class HoverController : MonoBehaviour, IStimuliHoverController
{
    [SerializeField] private Transform hoverGO;
    [SerializeField] private Color hoverColor;
    [SerializeField] private float hoverThickness = 1.2f;
    [SerializeField] private Color selectedColor;
    [SerializeField] private float selectedThickness = 1.3f;

    public bool isHovered = false;
    public bool isSelected = true;

    private SpriteRenderer hoverSpriteRenderer;
    private Vector3 startingScale;
    private  Color WHITE = new(255, 255, 255);

    void Awake()
    {
        hoverSpriteRenderer = hoverGO.GetComponent<SpriteRenderer>();
        startingScale = new(1,1,1);
        hoverGO.localScale = startingScale;

        if(hoverSpriteRenderer == null)
        {
            throw new MissingComponentException("Missing sprite renderer on hover game object");
        }
    }

    void Update()
    {
        if(isSelected)
        {
            hoverSpriteRenderer.color = selectedColor;
            hoverGO.localScale = startingScale * selectedThickness;
        }
        else if (isHovered)
        {
            hoverSpriteRenderer.color = hoverColor;
            hoverGO.localScale = startingScale * hoverThickness;
        }
        else
        { 
            hoverSpriteRenderer.color = WHITE;
            hoverGO.localScale = startingScale;
        }
    }

    private void Reset()
    {
        Debug.Log("RESETING");
        hoverSpriteRenderer.color = WHITE;
        hoverGO.localScale = startingScale;
    }

    public void SetIsHovered(bool on)
    {
        isHovered = on;
        // if (on)
        // {
        //     Debug.Log("HOVERING");
        //     hoverSpriteRenderer.color = hoverColor;
        //     hoverGO.localScale = startingScale * hoverThickness;
        // }
    }


    public void SetIsSelected(bool on)
    {
        isSelected = on;
        // if (on)
        // {
        //     Debug.Log("SELECTING");
        //     hoverSpriteRenderer.color = selectedColor;
        //     hoverGO.localScale = startingScale * selectedThickness;
        // }
    }

    public void SetThickness(float thickness)
    {
        hoverGO.localScale = startingScale * thickness;
    }
}

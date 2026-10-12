using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildingGhost : MonoBehaviour
{
    private const float Alpha = 0.5f;

    private readonly Color validPlacementColor = Color.green;
    private readonly Color idleColor = Color.yellow;
    private readonly Color invalidPlacementColor = Color.red;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetValidState()
    {
        spriteRenderer.color = new Color(validPlacementColor.r, validPlacementColor.g, validPlacementColor.b, Alpha);
    }

    public void SetIdleState()
    {
        spriteRenderer.color = new Color(idleColor.r, idleColor.g, idleColor.b, Alpha);
    }

    public void SetInvalidState()
    {
        spriteRenderer.color = new Color(invalidPlacementColor.r, invalidPlacementColor.g, invalidPlacementColor.b, Alpha);
    }
}

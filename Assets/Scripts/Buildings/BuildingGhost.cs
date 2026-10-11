using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildingGhost : MonoBehaviour
{
    private const float Alpha = 0.5f;
    private readonly Color validColor = Color.green;
    private readonly Color invalidColor = Color.red;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;

        spriteRenderer.color = new Color(validColor.r, validColor.g, validColor.b, Alpha);
    }
}

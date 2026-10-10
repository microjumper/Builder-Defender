using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private BuildingData buildingData;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(BuildingData data)
    {
        buildingData = data;

        spriteRenderer.sprite = buildingData.BuildingSprite;
    }
}

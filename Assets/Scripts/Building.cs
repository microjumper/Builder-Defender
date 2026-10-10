using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ResourceGenerator))]
public class Building : MonoBehaviour
{
    private BuildingData buildingData;

    private SpriteRenderer spriteRenderer;
    private ResourceGenerator resourceGenerator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        resourceGenerator = GetComponent<ResourceGenerator>();
    }

    public void Initialize(BuildingData data)
    {
        buildingData = data;

        spriteRenderer.sprite = buildingData.BuildingSprite;
        resourceGenerator.SetProductionData(buildingData.ProductionData);
    }
}

using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private const float TickInterval = 1f;

    private ResourceInventory resourceInventory;
    private ProductionData productionData;

    private bool requiresNearbyResource = true;

    private float elapsedTime = 0f;

    public void SetResourceInventory(ResourceInventory inventory) => resourceInventory = inventory;

    public void SetProductionData(ProductionData data, bool requiresNearby = true)
    {
        productionData = data;
        requiresNearbyResource = requiresNearby;
    }

    private void Update()
    {
        if (productionData == null || resourceInventory == null)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= TickInterval)
        {
            elapsedTime -= TickInterval;

            GenerateResource();
        }
    }

    private void GenerateResource()
    {
        if (!requiresNearbyResource)
        {
            resourceInventory.AddResource(productionData.ResourceData, productionData.ProductionPerSecond);
            
            return;
        }


    }
}

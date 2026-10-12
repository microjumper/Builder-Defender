using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private const float TickInterval = 1f;
    private const float DefaultDetectionRadius = 3f;

    private ResourceInventory resourceInventory;
    private ProductionData productionData;
    private float detectionRadius = DefaultDetectionRadius;
    private bool requiresNearbyResource = true;

    private float elapsedTime = 0f;

    public void SetResourceInventory(ResourceInventory inventory) => resourceInventory = inventory;

    public void SetProductionData(ProductionData data, float radius = DefaultDetectionRadius, bool requiresNearby = true)
    {
        productionData = data;
        detectionRadius = radius <= 0f ? DefaultDetectionRadius : radius;
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

        if (ResourceNode.TryFindNearest(transform.position, productionData.ResourceData, detectionRadius, out var node))
        {
            int harvested = node.Harvest(productionData.ProductionPerSecond);

            if (harvested > 0)
            {
                resourceInventory.AddResource(productionData.ResourceData, harvested);
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
#endif
}

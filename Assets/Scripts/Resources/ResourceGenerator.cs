using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceInventory resourceInventory;
    private ProductionData productionData;

    private float elapsedTime = 0f;

    public void SetResourceInventory(ResourceInventory inventory) => resourceInventory = inventory;
    public void SetProductionData(ProductionData data) => productionData = data;

    private void Update()
    {
        if (productionData != null)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= productionData.ProductionPerSecond)
            {
                elapsedTime -= productionData.ProductionPerSecond;

                GenerateResource();
            }
        }
    }

    private void GenerateResource()
    {
        if (resourceInventory != null)
        {
            resourceInventory.AddResource(productionData.ResourceData, productionData.ProductionPerSecond);
        }
    }
}

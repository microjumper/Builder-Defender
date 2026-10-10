using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ProductionData productionData;

    private float elapsedTime = 0f;

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
        Debug.Log($"Generated {productionData.ProductionPerSecond} of {productionData.ResourceData.ResourceName}");
    }
}

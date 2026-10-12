using UnityEngine;

public class BuildingFactory : MonoBehaviour
{
    public static BuildingFactory Instance { get; private set; }

    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private ResourceInventory resourceInventory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }

    public GameObject CreateBuilding(BuildingData buildingData, Vector2 position)
    {
        var buildingObject = Instantiate(buildingPrefab, position, Quaternion.identity);
        buildingObject.SetActive(false);

        if(buildingObject.TryGetComponent(out Building building))
        {
            building.SetBuildingData(buildingData);
        }

        if(buildingObject.TryGetComponent(out ResourceGenerator resourceGenerator))
        {
            resourceGenerator.SetResourceInventory(resourceInventory);
            resourceGenerator.SetProductionData(
                buildingData.ProductionData,
                buildingData.DetectionRadius,
                buildingData.RequiresNearbyResource);
        }

        buildingObject.SetActive(true);

        return buildingObject;
    }
}

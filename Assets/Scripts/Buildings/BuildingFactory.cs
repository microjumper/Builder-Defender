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

    public GameObject CreateBuilding(BuildingData buildingData, Vector2 position, ResourceNode resourceNode = null)
    {
        var buildingObject = Instantiate(buildingPrefab, position, Quaternion.identity);
        
        var buildingVisual = Instantiate(buildingData.BuildingVisual, buildingObject.transform);
       
        buildingObject.SetActive(false);

        if (buildingObject.TryGetComponent(out BoxCollider2D collider))
        {
            collider.size = buildingData.BuildingFootprint;
        }

        if (buildingObject.TryGetComponent(out HarvesterBuilding building))
        {
            building.Initialize(buildingData, resourceInventory, buildingVisual, resourceNode);
        }

        buildingObject.SetActive(true);

        return buildingObject;
    }
}

using UnityEngine;

// TODO: Refactor this to use a state machine

[RequireComponent(typeof(SpriteRenderer))]
public class HarvesterBuilding : MonoBehaviour
{
    private const float TickInterval = 1f;
    private float elapsedTime = 0f;

    private BuildingData buildingData;
    private ResourceInventory resourceInventory;
    private GameObject buildingVisual;

    private SpriteRenderer spriteRenderer;

    private ResourceNode currentNode;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (currentNode == null)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= TickInterval)
        {
            elapsedTime -= TickInterval;

            GenerateResource();
        }

        if (currentNode.IsDepleted)
        {
            ResourceNodeFinder.TryFindNearbyNode(
                transform.position, 
                buildingData.ReachArea(transform.position), 
                buildingData.ProductionData.ResourceData, 
                out ResourceNode node
            );
            
            currentNode = node;

            if (currentNode == null)
            {
                spriteRenderer.sprite = buildingData.BuildingSprite;
                buildingVisual.SetActive(false);
            }
        }
    }

    private void GenerateResource()
    {
        if (currentNode != null && !currentNode.IsDepleted)
        {
            currentNode.Harvest(buildingData.ProductionData.ProductionPerSecond);
            resourceInventory.AddResource(buildingData.ProductionData.ResourceData, buildingData.ProductionData.ProductionPerSecond);
        }
    }

    public void Initialize(BuildingData buildingData, ResourceInventory resourceInventory, GameObject buildingVisual)
    {
        this.buildingData = buildingData;
        this.resourceInventory = resourceInventory;
        this.buildingVisual = buildingVisual;

        spriteRenderer.sprite = buildingData.BuildingSprite;
    }

    public void SetCurrentNode(ResourceNode node)
    {
        currentNode = node;

        spriteRenderer.sprite = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, (Vector3)buildingData.ReachArea(transform.position).size);
    }
#endif
}

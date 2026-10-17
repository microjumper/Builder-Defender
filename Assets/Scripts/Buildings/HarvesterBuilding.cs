using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HarvesterBuilding : MonoBehaviour
{
    private enum State { Idle, Producing }

    private const float TickInterval = 1f;

    private BuildingData buildingData;
    private ResourceInventory resourceInventory;
    private GameObject buildingVisual;

    private SpriteRenderer spriteRenderer;

    private ResourceNode currentNode;
    private State? state;
    private float elapsedTime;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (state != State.Producing)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        TickProducing();
    }

    private void TickProducing()
    {
        if (currentNode == null || currentNode.IsDepleted)
        {
            AcquireNearbyNode();

            return;
        }

        if (elapsedTime < TickInterval)
        {
            return;
        }

        elapsedTime -= TickInterval;

        GenerateResource();
    }

    private void AcquireNearbyNode()
    {
        if (ResourceNodeFinder.TryFindNearbyNode(
                transform.position,
                buildingData.ReachArea(transform.position),
                buildingData.ProductionData.ResourceData,
                out ResourceNode node))
        {
            SetCurrentNode(node);
        }
        else
        {
            SetCurrentNode(null);
        }
    }

    private void GenerateResource()
    {
        currentNode.Harvest(buildingData.ProductionData.ProductionPerSecond);
        resourceInventory.AddResource(buildingData.ProductionData.ResourceData, buildingData.ProductionData.ProductionPerSecond);
    }

    private void SetState(State newState)
    {
        if (state == newState)
        {
            return;
        }

        state = newState;
        elapsedTime = 0f;

        switch (newState)
        {
            case State.Idle:
                spriteRenderer.sprite = buildingData.BuildingSprite;
                buildingVisual.SetActive(false);
                break;
            case State.Producing:
                spriteRenderer.sprite = null;
                buildingVisual.SetActive(true);
                break;
        }
    }

    private void SetCurrentNode(ResourceNode node)
    {
        currentNode = node;
        SetState(node != null ? State.Producing : State.Idle);
    }

    public void Initialize(BuildingData buildingData, ResourceInventory resourceInventory, GameObject buildingVisual, ResourceNode node = null)
    {
        this.buildingData = buildingData;
        this.resourceInventory = resourceInventory;
        this.buildingVisual = buildingVisual;

        SetCurrentNode(node);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (buildingData == null)
        {
            return;
        }

        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, (Vector3)buildingData.ReachArea(transform.position).size);
    }
#endif
}

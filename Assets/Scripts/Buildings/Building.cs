using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour
{
    private BuildingData buildingData;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetBuildingData(BuildingData data)
    {
        buildingData = data;

        spriteRenderer.sprite = buildingData.BuildingSprite;
    }

    public void DisableSprite() => spriteRenderer.enabled = false;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position, (Vector3)buildingData.ReachArea(transform.position).size);
    }
#endif
}

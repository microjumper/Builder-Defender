using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BuildingGhost : MonoBehaviour
{
    [Header("Colors for different placement states")]
    [SerializeField] private Color placementAllowedColor = Color.green;
    [SerializeField] private Color previewIdleColor = Color.yellow;
    [SerializeField] private Color placementDeniedColor = Color.red;
    [SerializeField] private float alpha = 0.5f;

    [Header("Event Channels")]
    [SerializeField] private BuildingSelected BuildingSelected;
    [SerializeField] private PlacementValidityChanged PlacementValidityChanged;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        BuildingSelected.EventRaised += OnBuildingSelected;
        PlacementValidityChanged.EventRaised += OnPlacementValidityChanged;
    }

    private void OnDisable()
    {
        BuildingSelected.EventRaised -= OnBuildingSelected;
        PlacementValidityChanged.EventRaised -= OnPlacementValidityChanged;
    }

    private void OnBuildingSelected(BuildingData buildingData)
    {
        if (buildingData != null)
        {
            spriteRenderer.sprite = buildingData.BuildingSprite;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }

    private void OnPlacementValidityChanged(PlacementState state)
    {
        var placementColor = state switch
        {
            PlacementState.Allowed => placementAllowedColor,
            PlacementState.Idle => previewIdleColor,
            PlacementState.Denied => placementDeniedColor,
            _ => Color.white
        };

        spriteRenderer.color = new Color(placementColor.r, placementColor.g, placementColor.b, alpha);
    }
}

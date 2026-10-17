using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayers;

    [SerializeField] private BuildingSelected BuildingSelected;
    [SerializeField] private PlacementValidityChanged PlacementValidityChanged;
    [SerializeField] private BuildingGhost buildingGhost;

    private Camera mainCamera;

    // Cached fields to eliminate GC allocations during clicks
    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;

    private BuildingData currentBuilding;

    private void Start()
    {
        mainCamera = Camera.main;

        pointerData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    private void OnEnable() => BuildingSelected.EventRaised += OnBuildingSelected;
    private void OnDisable() => BuildingSelected.EventRaised -= OnBuildingSelected;

    private void OnBuildingSelected(BuildingData buildingData) => currentBuilding = buildingData;

    private void Update()
    {
        if (currentBuilding != null)
        {
            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            buildingGhost.transform.position = mouseWorldPosition;

            if (CanBePlaced(mouseWorldPosition))
            {
                CheckProduction(currentBuilding, mouseWorldPosition);
            }
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }

    private bool CanBePlaced(Vector2 position)
    {
        if (Physics2D.OverlapBoxAll(position, currentBuilding.BuildingFootprint, 0f, obstacleLayers).Length > 0)
        {
            PlacementValidityChanged.RaiseEvent(PlacementState.Denied);
            
            return false;
        }

        return true;
    }

    private void CheckProduction(BuildingData buildingData, Vector2 position)
    {
        if (!buildingData.RequiresNearbyResource || 
            ResourceNodeFinder.TryFindNearbyNode(position, buildingData.ReachArea(position), buildingData.ProductionData.ResourceData, out _))
        {
            PlacementValidityChanged.RaiseEvent(PlacementState.Allowed);
        }
        else
        {
            PlacementValidityChanged.RaiseEvent(PlacementState.Idle);
        }
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed && !IsPointerOverUI())
        {
            if (currentBuilding == null)
            {
                Debug.LogWarning("No building selected. Please select a building using the hotkeys.");
                return;
            }

            // Recompute at click time so a mouse move since the last Update can't slip through.
            Vector2 clickPosition = GetMouseWorldPosition();

            if (CanBePlaced(clickPosition))
            {
                if (ResourceNodeFinder.TryFindNearbyNode(
                    clickPosition,
                    currentBuilding.ReachArea(clickPosition),
                    currentBuilding.ProductionData.ResourceData,
                    out ResourceNode resourceNode))
                {
                    BuildingFactory.Instance.CreateBuilding(currentBuilding, clickPosition, resourceNode);
                }
                else
                {
                    BuildingFactory.Instance.CreateBuilding(currentBuilding, clickPosition);
                }
            }
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentBuilding = null;

            BuildingSelected.RaiseEvent(null);
        }
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;

        pointerData.position = Mouse.current != null ? Mouse.current.position.ReadValue() : (Vector2)Input.mousePosition;

        raycastResults.Clear();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Count > 0;
    }
}

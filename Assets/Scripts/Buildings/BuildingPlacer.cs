using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingSelected BuildingSelected;
    [SerializeField] private PlacementValidityChanged PlacementValidityChanged;
    [SerializeField] private BuildingGhost buildingGhost;

    private Camera mainCamera;

    // Cached fields to eliminate GC allocations during clicks
    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;

    private BuildingData currentBuilding;

    // Authoritative placement state, recomputed each frame while a ghost is active.
    private bool canPlace = false;

    private void Start()
    {
        mainCamera = Camera.main;

        pointerData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    private void OnEnable() => BuildingSelected.EventRaised += OnBuildingSelected;
    private void OnDisable() => BuildingSelected.EventRaised -= OnBuildingSelected;

    private void Update()
    {
        if (currentBuilding != null)
        {
            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            buildingGhost.transform.position = mouseWorldPosition;
            CheckPlacement(mouseWorldPosition);
        }
    }

    private void CheckPlacement(Vector2 position)
    {
        canPlace = BuildingPlacementRules.IsValid(currentBuilding, position);

        if (canPlace)
        {
            if (BuildingPlacementRules.WillProduce(currentBuilding, position))
            {
                PlacementValidityChanged.RaiseEvent(PlacementState.Allowed);

            }
            else
            {
                PlacementValidityChanged.RaiseEvent(PlacementState.Idle);
            }
        }
        else
        {
            PlacementValidityChanged.RaiseEvent(PlacementState.Denied);
        }
    }

    private void OnBuildingSelected(BuildingData buildingData)
    {
        currentBuilding = buildingData;
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

            if (!BuildingPlacementRules.IsValid(currentBuilding, clickPosition))
            {
                return;
            }

            BuildingFactory.Instance.CreateBuilding(currentBuilding, clickPosition);
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

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }
}

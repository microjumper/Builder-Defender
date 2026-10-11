using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingSelected buildingSelected;
    private Camera mainCamera;

    // Cache these fields to eliminate GC allocations during clicks
    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;

    private BuildingData currentBuilding;

    private void Start()
    {
        mainCamera = Camera.main;

        pointerData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    private void OnEnable() => buildingSelected.EventRaised += OnBuildingSelected;
    private void OnDisable() => buildingSelected.EventRaised -= OnBuildingSelected;

    private void OnBuildingSelected(BuildingData buildingData) => currentBuilding = buildingData;

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed && !IsPointerOverUI())
        {
            if (currentBuilding != null)
            {
                Vector2 screenPosition = Mouse.current.position.ReadValue();
                Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

                BuildingFactory.Instance.CreateBuilding(currentBuilding, worldPosition);
            }
            else
            {
                Debug.LogWarning("No building selected. Please select a building using the hotkeys.");
            }
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private GameObject buildingGhostPrefab;
    [SerializeField] private BuildingSelected buildingSelected;
    private Camera mainCamera;

    // Cached fields to eliminate GC allocations during clicks
    private PointerEventData pointerData;
    private List<RaycastResult> raycastResults;

    private BuildingData currentBuilding;
    private BuildingGhost buildingGhost;

    private void Start()
    {
        mainCamera = Camera.main;

        pointerData = new PointerEventData(EventSystem.current);
        raycastResults = new List<RaycastResult>();
    }

    private void OnEnable() => buildingSelected.EventRaised += OnBuildingSelected;
    private void OnDisable() => buildingSelected.EventRaised -= OnBuildingSelected;

    private void Update() => MakeGhostFollowMouse();

    private void MakeGhostFollowMouse()
    {
        if (buildingGhost != null)
        {
            Vector2 mouseWorldPosition = GetMouseWorldPosition();
            buildingGhost.transform.position = mouseWorldPosition;
        }
    }

    private void OnBuildingSelected(BuildingData buildingData)
    {
        if (buildingGhost != null)
        {
            Destroy(buildingGhost.gameObject);
        }

        currentBuilding = buildingData;

        buildingGhost = Instantiate(buildingGhostPrefab, GetMouseWorldPosition(), Quaternion.identity).GetComponent<BuildingGhost>();
        buildingGhost.SetSprite(currentBuilding.BuildingSprite);
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed && !IsPointerOverUI())
        {
            if (currentBuilding != null)
            {
                BuildingFactory.Instance.CreateBuilding(currentBuilding, GetMouseWorldPosition());
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

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        return mainCamera.ScreenToWorldPoint(screenPosition);
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    private Camera mainCamera;

    private BuildingData currentBuilding;

    private void Start() => mainCamera = Camera.main;

    private void OnEnable() => HotkeyInput.BuildingSelected += OnBuildingSelected;
    private void OnDisable() => HotkeyInput.BuildingSelected -= OnBuildingSelected;

    private void OnBuildingSelected(BuildingData buildingData) => currentBuilding = buildingData;

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            if (currentBuilding == null)
            {
                Debug.LogWarning("No building selected. Please select a building using the hotkeys.");
                return;
            }

            BuildingFactory.Instance.CreateBuilding(currentBuilding, worldPosition);
        }
    }
}
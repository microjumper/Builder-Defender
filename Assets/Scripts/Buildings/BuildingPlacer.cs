using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingSelected buildingSelected;
    private Camera mainCamera;

    private BuildingData currentBuilding;

    private void Start() => mainCamera = Camera.main;

    private void OnEnable() => buildingSelected.EventRaised += OnBuildingSelected;
    private void OnDisable() => buildingSelected.EventRaised -= OnBuildingSelected;

    private void OnBuildingSelected(BuildingData buildingData) => currentBuilding = buildingData;

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
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
}
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingRegistry buildingRegistry;
    [SerializeField] private GameObject buildingPrefab;

    private Camera mainCamera;
    private BuildingData currentBuilding;

    private void Start()
    {
        mainCamera = Camera.main;
    }

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

            var building = Instantiate(buildingPrefab, worldPosition, Quaternion.identity);
            building.GetComponent<Building>().Initialize(currentBuilding);
        }
    }

    public void OnBuildingHotkey(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.control is KeyControl keyControl)
            {
                Key hotkey = keyControl.keyCode;

                if (buildingRegistry.BuildingsByHotkey.TryGetValue(hotkey, out var buildingData))
                {
                    currentBuilding = buildingData;
                    Debug.Log($"Hotkey '{hotkey}' selected: {buildingData.BuildingName}");
                }
                else
                {
                    currentBuilding = null;
                    Debug.Log($"No building assigned to hotkey '{hotkey}'.");
                }
            }
        }
    }
}

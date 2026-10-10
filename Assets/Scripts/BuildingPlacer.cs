using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingRegistry buildingRegistry;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

            Debug.Log("Left click detected at: " + screenPosition);
            Debug.Log("Screen to World Position: " + worldPosition);
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
                    Debug.Log($"Hotkey '{hotkey}' selected: {buildingData.BuildingName}");
                }
                else
                {
                    Debug.Log($"No building assigned to hotkey '{hotkey}'.");
                }
            }
        }
    }
}

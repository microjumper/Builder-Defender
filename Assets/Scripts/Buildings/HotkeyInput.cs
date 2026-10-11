using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class HotkeyInput : MonoBehaviour
{
    [SerializeField] private BuildingRegistry buildingRegistry;
    [SerializeField] private BuildingSelected buildingSelected;

    public void OnBuildingHotkey(InputAction.CallbackContext context)
    {
        if (context.performed && context.control is KeyControl keyControl)
        {
            var hotkey = keyControl.keyCode;

            if (buildingRegistry.BuildingsByHotkey.TryGetValue(hotkey, out var buildingData))
            {
                buildingSelected.RaiseEvent(buildingData);
            }
            else
            {
                buildingSelected.RaiseEvent(null);

                Debug.LogWarning($"No building assigned to hotkey '{hotkey}'.");
            }
        }
    }
}
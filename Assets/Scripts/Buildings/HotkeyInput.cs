using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class HotkeyInput : MonoBehaviour
{
    [SerializeField] private BuildingRegistry buildingRegistry;

    public static event Action<BuildingData> BuildingSelected;

    public void OnBuildingHotkey(InputAction.CallbackContext context)
    {
        if (context.performed && context.control is KeyControl keyControl)
        {
            var hotkey = keyControl.keyCode;

            if (buildingRegistry.BuildingsByHotkey.TryGetValue(hotkey, out var buildingData))
            {
                BuildingSelected?.Invoke(buildingData);

                Debug.Log($"Hotkey '{hotkey}' selected: {buildingData.BuildingName}");
            }
            else
            {
                BuildingSelected?.Invoke(null);

                Debug.Log($"No building assigned to hotkey '{hotkey}'.");
            }
        }
    }
}
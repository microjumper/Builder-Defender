using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingRegistry", menuName = "Scriptable Objects/BuildingRegistry")]
public class BuildingRegistry : ScriptableObject
{
    public IReadOnlyDictionary<Key, BuildingData> BuildingsByHotkey => buildingsByHotkey;

    [SerializeField] private List<BuildingData> buildings;

    private readonly Dictionary<Key, BuildingData> buildingsByHotkey = new();

    private void OnEnable() => RebuildDictionary();
    private void OnValidate() => RebuildDictionary();

    private void RebuildDictionary()
    {
        buildingsByHotkey.Clear();

        if (buildings != null)
        {
            foreach (var building in buildings)
            {
                if (building == null) continue;

                if (!buildingsByHotkey.TryAdd(building.BuildingHotkey, building))
                {
                    Debug.LogWarning($"Duplicate hotkey '{building.BuildingHotkey}' found for building '{building.BuildingName}'.");
                }
            }
        }
    }
}
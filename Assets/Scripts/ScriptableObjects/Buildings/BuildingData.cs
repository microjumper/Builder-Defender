using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Buildings/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite buildingSprite;
    [SerializeField] private GameObject buildingVisual;
    [SerializeField] private Key buildingHotkey;
    [SerializeField] private ProductionData productionData;
    [SerializeField] private Dictionary<ResourceData, int> resourceRequirements = new();
    [SerializeField] private Vector2 buildingFootprint = new (9, 8);

    [Header("Gathering")]
    [SerializeField, Min(0f)] private float detectionDistance = 3f;
    [Tooltip("If true, building only produces when a matching ResourceNode is within this radius. Idle otherwise.")]
    [SerializeField] private bool requiresNearbyResource = true;

    public string BuildingName => buildingName;
    public Sprite BuildingSprite => buildingSprite;
    public GameObject BuildingVisual => buildingVisual;
    public Key BuildingHotkey => buildingHotkey;
    public ProductionData ProductionData => productionData;
    public Vector2 BuildingFootprint => buildingFootprint;
    public ReadOnlyDictionary<ResourceData, int> ResourceRequirements => new(resourceRequirements);
    public float DetectionDistance => detectionDistance;
    public bool RequiresNearbyResource => requiresNearbyResource;

    public Rect ReachArea(Vector2 center)
    {
        Vector2 size = buildingFootprint + Vector2.one * (2 * detectionDistance);
        return new Rect(center - size * 0.5f, size);
    }
}

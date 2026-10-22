using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Buildings/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string buildingName;
    public string BuildingName => buildingName;

    [SerializeField] private Sprite buildingSprite;
    public Sprite BuildingSprite => buildingSprite;

    [SerializeField] private GameObject buildingVisual;
    public GameObject BuildingVisual => buildingVisual;

    [SerializeField] private Key buildingHotkey;
    public Key BuildingHotkey => buildingHotkey;

    [SerializeField] private ProductionData productionData;
    public ProductionData ProductionData => productionData;

    [SerializeField] private Dictionary<ResourceData, int> resourceRequirements = new();
    public ReadOnlyDictionary<ResourceData, int> ResourceRequirements => new(resourceRequirements);
    
    [SerializeField] private Vector2 buildingFootprint = new (9, 8);
    public Vector2 BuildingFootprint => buildingFootprint;

    [Header("Gathering")]
    [SerializeField, Min(0f)] private float detectionDistance = 3f;
    public float DetectionDistance => detectionDistance;
    [Tooltip("If true, building only produces when a matching ResourceNode is within this radius. Idle otherwise.")]
    [SerializeField] private bool requiresNearbyResource = true;
    public bool RequiresNearbyResource => requiresNearbyResource;

    public Rect ReachArea(Vector2 center)
    {
        Vector2 size = buildingFootprint + Vector2.one * (2 * detectionDistance);
        return new Rect(center - size * 0.5f, size);
    }
}

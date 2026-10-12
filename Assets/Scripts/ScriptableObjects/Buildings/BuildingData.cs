using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Buildings/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite buildingSprite;
    [SerializeField] private Key buildingHotkey;
    [SerializeField] private ProductionData productionData;

    [Header("Gathering")]
    [SerializeField, Min(0f)] private float detectionRadius = 3f;
    [Tooltip("If true, building only produces when a matching ResourceNode is within this radius. Idle otherwise.")]
    [SerializeField] private bool requiresNearbyResource = true;

    public string BuildingName => buildingName;
    public Sprite BuildingSprite => buildingSprite;
    public Key BuildingHotkey => buildingHotkey;
    public ProductionData ProductionData => productionData;
    public float DetectionRadius => detectionRadius;
    public bool RequiresNearbyResource => requiresNearbyResource;
}

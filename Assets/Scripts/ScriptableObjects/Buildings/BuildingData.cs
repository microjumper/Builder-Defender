using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/Buildings/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite buildingSprite;
    [SerializeField] private Key buildingHotkey;
    [SerializeField] private ProductionData productionData;

    public string BuildingName => buildingName;
    public Sprite BuildingSprite => buildingSprite;
    public Key BuildingHotkey => buildingHotkey;
    public ProductionData ProductionData => productionData;
}

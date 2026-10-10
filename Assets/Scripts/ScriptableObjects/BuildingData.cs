using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite buildingSprite;
    [SerializeField] private Key buildingHotkey;

    public string BuildingName => buildingName;
    public Sprite BuildingSprite => buildingSprite;
    public Key BuildingHotkey => buildingHotkey;
}

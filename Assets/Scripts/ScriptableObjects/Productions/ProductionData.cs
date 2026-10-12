using UnityEngine;

[CreateAssetMenu(fileName = "ProductionData", menuName = "Scriptable Objects/Production/ProductionData")]
public class ProductionData : ScriptableObject
{
    [SerializeField] private ResourceData resourceData;
    [Tooltip("Amount harvested from a nearby ResourceNode per 1-second tick. Requires a matching node in range (flat rate if >=1 node).")]
    [SerializeField, Min(1)] private int productionPerSecond = 1;

    public ResourceData ResourceData => resourceData;
    public int ProductionPerSecond => productionPerSecond;
}

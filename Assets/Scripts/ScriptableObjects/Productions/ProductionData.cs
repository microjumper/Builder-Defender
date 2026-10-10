using UnityEngine;

[CreateAssetMenu(fileName = "ProductionData", menuName = "Scriptable Objects/Production/ProductionData")]
public class ProductionData : ScriptableObject
{
    [SerializeField] private ResourceData resourceData;
    [SerializeField] private int productionPerSecond;

    public ResourceData ResourceData => resourceData;
    public int ProductionPerSecond => productionPerSecond;
}

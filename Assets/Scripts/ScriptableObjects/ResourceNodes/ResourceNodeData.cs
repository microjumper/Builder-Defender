using UnityEngine;

[CreateAssetMenu(fileName = "ResourceNodeData", menuName = "Scriptable Objects/ResourceNodes/ResourceNodeData")]
public class ResourceNodeData : ScriptableObject
{
    [SerializeField] private ResourceData resourceData;
    [SerializeField] private GameObject resourceVisual;
    [SerializeField, Min(1)] private int resourceAmount = 10;

    public string ResourceName => resourceData.ResourceName;
    public GameObject ResourceVisual => resourceVisual;
    public int ResourceAmount => resourceAmount;
}

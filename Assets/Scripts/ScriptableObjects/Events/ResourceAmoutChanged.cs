using UnityEngine;

[CreateAssetMenu(fileName = "ResourceAmoutChanged", menuName = "Scriptable Objects/Events/ResourceAmoutChanged")]
public sealed class ResourceAmoutChanged : EventChannelData<ResourceAmontPayload> { }

public readonly struct ResourceAmontPayload
{
    public ResourceData ResourceData { get; }
    public int Amount { get; }

    public ResourceAmontPayload(ResourceData resourceData, int newAmount)
    {
        ResourceData = resourceData;
        Amount = newAmount;
    }
}

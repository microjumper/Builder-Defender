using UnityEngine;

[CreateAssetMenu(fileName = "ResourceAmountChanged", menuName = "Scriptable Objects/Events/ResourceAmountChanged")]
public sealed class ResourceAmountChanged : EventChannelData<ResourceAmontPayload> { }

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

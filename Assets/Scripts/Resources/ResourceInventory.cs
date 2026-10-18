using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    [SerializeField] private ResourceAmountChanged ResourceAmountChanged;

    [SerializeField] private Dictionary<ResourceData, int> resourceInventory = new();

    private void Start()
    {
        foreach (var (resource, amount) in resourceInventory)
        {
            ResourceAmountChanged.RaiseEvent(new ResourceAmontPayload(resource, amount));
        }
    }

    public int GetAmount(ResourceData resourceData)
    {
        ValidateResource(resourceData);

        return resourceInventory.TryGetValue(resourceData, out int amount) ? amount : 0;
    }

    private static void ValidateResource(ResourceData resourceData)
    {
        if (resourceData == null)
        {
            throw new ArgumentNullException(nameof(resourceData));
        }
    }

    public int AddResource(ResourceData resourceData, int amount)
    {
        ValidateResource(resourceData);
        ValidatePositiveAmount(amount);

        int newTotal = checked(GetAmount(resourceData) + amount);

        resourceInventory[resourceData] = newTotal;
        ResourceAmountChanged.RaiseEvent(new ResourceAmontPayload(resourceData, newTotal));

        return newTotal;
    }

    private static void ValidatePositiveAmount(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount must be positive.");
        }
    }

    public bool CanAfford(IReadOnlyDictionary<ResourceData, int> resourceRequirements)
    {
        foreach (var (resource, amount) in resourceRequirements)
        {
            if (GetAmount(resource) < amount)
            {
                return false;
            }
        }

        return true;
    }

    public bool TrySpend(IReadOnlyDictionary<ResourceData, int> resourceRequirements)
    {
        if (CanAfford(resourceRequirements))
        {
            foreach (var (resource, amount) in resourceRequirements)
            {
                SetAmount(resource, GetAmount(resource) - amount);
            }

            return true;
        }

        return false;
    }

    private void SetAmount(ResourceData resource, int newAmount)
    {
        resourceInventory[resource] = newAmount;
        ResourceAmountChanged.RaiseEvent(new ResourceAmontPayload(resource, newAmount));
    }
}

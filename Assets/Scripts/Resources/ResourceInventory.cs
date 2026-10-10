using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    public event Action<ResourceData, int> ResourceChanged;

    [SerializeField] private List<ResourceData> resourceDataList;

    private readonly Dictionary<ResourceData, int> amounts = new();

    private void Awake()
    {
        foreach (var resourceData in resourceDataList)
        {
            amounts[resourceData] = 0;
        }
    }

    public int GetAmount(ResourceData resourceData)
    {
        ValidateResource(resourceData);

        return amounts.TryGetValue(resourceData, out int amount) ? amount : 0;
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

        amounts[resourceData] = newTotal;
        ResourceChanged?.Invoke(resourceData, newTotal);

        return newTotal;
    }

    private static void ValidatePositiveAmount(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), amount, "Amount must be positive.");
        }
    }

    public bool TrySpendResource(ResourceData resourceData, int amount)
    {
        ValidateResource(resourceData);
        ValidatePositiveAmount(amount);

        int currentAmount = GetAmount(resourceData);

        if (currentAmount < amount)
        {
            return false;
        }

        int newTotal = currentAmount - amount;
        amounts[resourceData] = newTotal;

        ResourceChanged?.Invoke(resourceData, newTotal);

        return true;
    }
}

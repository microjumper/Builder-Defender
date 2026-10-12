using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [Header("Definition")]
    [SerializeField] private ResourceData resourceData;

    [Header("Capacity")]
    [SerializeField, Min(1)] private int initialAmount = 100;

    private int currentAmount;

    private void Awake()
    {
        currentAmount = initialAmount;
    }

    public int Harvest(int amount)
    {
        int harvestedAmount = 0;

        if (amount > 0 && currentAmount > 0)
        {
            harvestedAmount = Mathf.Min(amount, currentAmount);
            currentAmount -= harvestedAmount;
        }

        return harvestedAmount;
    }
}

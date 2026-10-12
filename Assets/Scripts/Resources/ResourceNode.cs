using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    private static readonly HashSet<ResourceNode> allNodes = new();

    public static IReadOnlyCollection<ResourceNode> AllNodes => allNodes;

    [Header("Definition")]
    [SerializeField] private ResourceData resourceData;

    [Header("Capacity")]
    [SerializeField, Min(1)] private int initialAmount = 100;

    private int currentAmount;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public ResourceData ResourceData => resourceData;
    public int CurrentAmount => currentAmount;
    public int InitialAmount => initialAmount;
    public bool IsDepleted => currentAmount <= 0;

    private void Awake()
    {
        currentAmount = initialAmount;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void OnEnable() => allNodes.Add(this);
    private void OnDisable() => allNodes.Remove(this);

    public int Harvest(int amount)
    {
        int harvestedAmount = 0;

        if (amount > 0 && currentAmount > 0)
        {
            harvestedAmount = Mathf.Min(amount, currentAmount);
            currentAmount -= harvestedAmount;

            if (IsDepleted)
            {
                OnDepleted();
            }
        }

        return harvestedAmount;
    }

    private void OnDepleted()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor * 0.4f;
        }
    }

    public static bool TryFindNearest(Vector2 position, ResourceData resourceData, float radius, out ResourceNode nearest)
    {
        nearest = null;
        float nearestSqr = radius * radius;

        foreach (var node in allNodes)
        {
            if (node == null || !node.isActiveAndEnabled || node.IsDepleted)
            {
                continue;
            }

            if (resourceData != null && node.resourceData != resourceData)
            {
                continue;
            }

            float sqr = ((Vector2)node.transform.position - position).sqrMagnitude;

            if (sqr <= nearestSqr)
            {
                // Flat rate: first node in range is enough, prefer closest for shared-node fairness.
                if (nearest == null || sqr < ((Vector2)nearest.transform.position - position).sqrMagnitude)
                {
                    nearest = node;
                    nearestSqr = sqr;
                }
            }
        }

        return nearest != null;
    }

    public static bool HasNodeInRange(Vector2 position, ResourceData resourceData, float radius)
        => TryFindNearest(position, resourceData, radius, out _);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = IsDepleted ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
#endif
}

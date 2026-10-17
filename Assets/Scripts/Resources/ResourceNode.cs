using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public ResourceNodeData ResourceNodeData => resourceNodeData;
    [SerializeField] private ResourceNodeData resourceNodeData;

    private int currentAmount;

    public bool IsDepleted => currentAmount <= 0;

    private void Awake()
    {
        currentAmount = resourceNodeData.ResourceAmount;

        Instantiate(resourceNodeData.ResourceVisual, transform.position, Quaternion.identity, transform);
    }

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
        Debug.Log($"Resource node '{resourceNodeData.ResourceData.ResourceName}' is depleted.");
        
        Destroy(gameObject);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = IsDepleted ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
#endif
}

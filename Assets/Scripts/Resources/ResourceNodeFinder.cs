using UnityEngine;

public static class ResourceNodeFinder
{
    public static bool TryFindNearbyNode(Vector2 position, Rect area, ResourceData resourceData, out ResourceNode node)
    {
        node = null;

        if (Physics2D.OverlapBoxAll(position, area.size, 0f) is Collider2D[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<ResourceNode>(out var resourceNode))
                {
                    if (resourceNode.ResourceNodeData.ResourceData == resourceData)
                    {
                        node = resourceNode;
                        
                        return true;
                    }
                }

            }
        }

        return false;
    }
}

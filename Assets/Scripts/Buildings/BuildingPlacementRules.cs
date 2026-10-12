using UnityEngine;

public static class BuildingPlacementRules
{
    /// <summary>
    /// Authoritative gate: may the player place a building of <paramref name="buildingData"/>
    /// at <paramref name="position"/>? This is the single seam for future blocking rules
    /// (overlap with existing buildings, off-map, resource cost, etc.).
    /// Currently lenient: placement is always allowed.
    /// </summary>
    public static bool IsValid(BuildingData buildingData, Vector2 position)
    {
        if (buildingData == null)
        {
            return false;
        }

        // Future blocking rules go here, e.g.:
        // if (OverlapsExistingBuilding(position)) return false;

        return true;
    }

    /// <summary>
    /// Informational prediction (not a gate): will a building of <paramref name="buildingData"/>
    /// produce when placed at <paramref name="position"/>? Drives the ghost color only.
    /// </summary>
    public static bool WillProduce(BuildingData buildingData, Vector2 position)
    {
        if (buildingData == null)
        {
            return false;
        }

        if (!buildingData.RequiresNearbyResource)
        {
            return true;
        }

        return ResourceNode.HasNodeInRange(
            position,
            buildingData.ProductionData.ResourceData,
            buildingData.DetectionRadius);
    }
}
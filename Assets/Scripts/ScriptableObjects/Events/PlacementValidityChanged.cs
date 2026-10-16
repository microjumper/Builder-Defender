using UnityEngine;

[CreateAssetMenu(fileName = "PlacementValidityChanged", menuName = "Scriptable Objects/Events/PlacementValidityChanged")]
public class PlacementValidityChanged : EventChannelData<PlacementState> { }

public enum PlacementState { Allowed, Idle, Denied }

using UnityEngine;

[CreateAssetMenu(fileName = "Destroyed", menuName = "Scriptable Objects/Events/Destroyed")]
public class Destroyed : EventChannelData<DestructionInfo> { }

public readonly struct DestructionInfo { }
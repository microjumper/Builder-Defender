using UnityEngine;

[CreateAssetMenu(fileName = "ResourceData", menuName = "Scriptable Objects/Resources/ResourceData")]
public class ResourceData : ScriptableObject
{
    [SerializeField] private string resourceName;
    [SerializeField] private Sprite resourceSprite;

    public string ResourceName => resourceName;
    public Sprite ResourceSprite => resourceSprite;
}

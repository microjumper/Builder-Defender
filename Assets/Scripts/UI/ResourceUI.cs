using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceAmoutChanged resourceAmountChanged;
    [SerializeField] private ResourceData resourceData;
    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI resourceText;

    private void Awake()
    {
        resourceImage.sprite = resourceData.ResourceSprite;
        resourceText.text = "0";
    }

    private void OnEnable() => resourceAmountChanged.EventRaised += OnResourceAmoutChanged;
    private void OnDisable() => resourceAmountChanged.EventRaised -= OnResourceAmoutChanged;

    private void OnResourceAmoutChanged(ResourceAmontPayload payload)
    {
        if (payload.ResourceData == resourceData)
        {
            resourceText.text = payload.Amount.ToString();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (resourceImage != null && resourceData != null)
        {
            resourceImage.sprite = resourceData.ResourceSprite;
        }
    }
#endif
}

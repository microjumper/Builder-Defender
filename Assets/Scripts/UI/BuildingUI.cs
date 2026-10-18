using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private ResourceInventory resourceInventory;
    [SerializeField] private ResourceAmountChanged ResourceAmountChanged;
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private BuildingSelected buildingSelected;

    [SerializeField] private Image buildingImage;
    [SerializeField] private Button buildingButton;
    [SerializeField] private TextMeshProUGUI buildingHotkey;

    private void Awake()
    {
        buildingImage.sprite = buildingData.BuildingSprite;
        buildingHotkey.text = buildingData.BuildingHotkey.ToString();

        buildingButton.interactable = false;
    }

    private void OnEnable() => ResourceAmountChanged.EventRaised += OnResourceAmountChanged;

    private void OnDisable() => ResourceAmountChanged.EventRaised -= OnResourceAmountChanged;

    private void OnResourceAmountChanged(ResourceAmontPayload payload)
    {
        if (buildingData.ResourceRequirements.ContainsKey(payload.ResourceData))
        {
            if (resourceInventory.CanAfford(buildingData.ResourceRequirements))
            {
                buildingButton.interactable = true;
            }
            else
            {
                buildingButton.interactable = false;
            }
        }
    }

    public void OnClick()
    {
        buildingSelected.RaiseEvent(buildingData);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (buildingImage != null && buildingData != null)
        {
            buildingImage.sprite = buildingData.BuildingSprite;
        }

        if (buildingHotkey != null && buildingData != null)
        {
            buildingHotkey.text = buildingData.BuildingHotkey.ToString();
        }
    }
#endif
}

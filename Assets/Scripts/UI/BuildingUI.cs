using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private BuildingSelected buildingSelected;

    [SerializeField] private Image buildingImage;
    [SerializeField] private Button buildingButton;
    [SerializeField] private TextMeshProUGUI buildingHotkey;

    private void Awake()
    {
        buildingImage.sprite = buildingData.BuildingSprite;
        buildingHotkey.text = buildingData.BuildingHotkey.ToString();

        //buildingButton.interactable = false;
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

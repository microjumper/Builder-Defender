using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Color healthColor = Color.red;

    private void OnEnable()
    {
        barImage.color = healthColor;
        
        SetFillAmount(1);
    }

    public void SetFillAmount(float amount)
    {
        if (amount >= 0 && amount <= 1)
        {
            barImage.fillAmount = amount;
        }
    }
}

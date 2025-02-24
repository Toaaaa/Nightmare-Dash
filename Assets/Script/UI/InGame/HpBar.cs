using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;

    public void SetHpBar(float max)
    {
        hpBar.maxValue = max;
        hpBar.value = max;
    }
    
    public void UpdateHpBar(float current)
    {
        hpBar.value = current;
    }
}

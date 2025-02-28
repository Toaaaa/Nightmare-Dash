using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    public void GetDmg(float hppercent) // 체력 비율 받아서 hp바 갱신
    {
        RectTransform hpRect = hpBar.GetComponent<RectTransform>();
        hpRect.DOShakeAnchorPos(0.8f, new Vector2(2f, 0f), 10, 90);
        hpBar.DOValue(hpBar.maxValue * hppercent, 0.5f);
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;

    private Hp hp = new();

    private void Start()
    {
        hp.OnChangedHpAmountEvent += UpdateHpBar;
    }

    public void SetHpBar(float max)
    {
        hpBar.maxValue = max;
        hpBar.value = max;

        hp.SetHp(max);
    }
    
    public void UpdateHpBar(float current)
    {
        hpBar.value = current;
    }

    public void Heal(float add)
    {
        hp.InCrease(add);
    }

    public void Damage(float damage)
    {
        hp.Decrease(damage);
    }
}

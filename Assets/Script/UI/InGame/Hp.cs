using System;

public class Hp
{
    public float Current
    {
        get => current;
        set
        {
            current = value;
            OnChangedHpAmountEvent?.Invoke(current);
        }
    }

    private float current = 0.0f;

    public float MaxHp;
    private float minHp = 0.0f;

    //체력 변화 이벤트
    public Action<float> OnChangedHpAmountEvent = delegate { };

    //체력 설정
    public void SetHp(float max)
    {
        current = max;
        MaxHp = max;
    }

    public void UpdateHp(float hp)
    {
        Current = hp;
    }

    //체력 증가
    public void InCrease(float hp)
    {
        if (Current + hp <= MaxHp)
        {
            Current += hp;
        }
        else
        {
            Current = MaxHp;
        }
    }

    //체력 감소
    public void Decrease(float hp)
    {
        if (Current - hp >= 0)
        {
            Current -= hp;
        }
        else
        {
            Current = minHp;
        }
    }
}

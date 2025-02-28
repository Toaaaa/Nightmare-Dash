using System;
using UnityEngine;

public class Diamond
{
    public long Current
    {
        get => current;
        private set
        {
            current = value;
            OnChangedDiamondAmountEvent(current);
        }
    }

    private long current;

    public Action<long> OnChangedDiamondAmountEvent = delegate { };

    //다이아 획득
    public void Add(long amount)
    {
        if (amount > 0)
        {
            Current += amount;
        }
    }

    //다이아 사용
    public void Use(long amount)
    {
        if (IsCanUse(amount))
        {
            Current -= amount;
        }
    }

    public void Set(long amount)
    {
        Current = amount;
    }

    //다이아를 사용할수있는지 판단
    public bool IsCanUse(long amount)
    {
        return Current - amount >= 0;
    }
}

using System;

public class Coin
{
    public long Current
    {
        get => current;
        private set
        {
            current = value;
            OnChangedCoinAmountEvent(current);
        }
    }

    private long current;

    public Action<long> OnChangedCoinAmountEvent = delegate { };

    //코인 획득
    public void Add(long amount)
    {
        if (amount > 0)
        {
            Current += amount;
        }
    }

    //코인 사용
    public void Use(long amount)
    {
        if (IsCanUse(amount))
        {
            Current -= amount;
        }
    }

    //코인을 사용할수있는지 판단
    public bool IsCanUse(long amount)
    {
        return Current - amount >= 0;
    }

}

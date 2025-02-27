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

    private long exchangePrice = 10;

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

    //교환할수있는지 판단
    public bool IsExchange()
    {
        return Current != 0 && Current % exchangePrice == 0;
    }

    //교환 가능한 다이아몬드 반환
    public long GetExchangedDiamondAmount()
    {
        if (IsExchange())
        {
            long exchangedDiamond = Current / exchangePrice; //교환된 다이아
            Current %= exchangePrice;
            Use(exchangedDiamond * exchangePrice); // 코인사용
            long currentdiamond = exchangedDiamond * exchangePrice;
            DataManager.Instance.Diamond.Add(exchangedDiamond);
            return exchangedDiamond;
        }
        return 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeBtn : MonoBehaviour
{
    public Button button;

    public void Start()
    {
        button.onClick.AddListener(Exchange);
    }

    public void Exchange()
    {
        Coin coin = DataManager.Instance.Coin;
        coin.GetExchangedDiamondAmount();
    }
}

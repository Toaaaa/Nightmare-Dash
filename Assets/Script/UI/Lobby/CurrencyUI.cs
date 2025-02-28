using TMPro;
using UnityEngine;
public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text diaText;

    private Coin playerCoin;
    private Diamond playerDiamond;

    private void Start()
    {
        playerCoin = DataManager.Instance.Coin;
        playerDiamond = DataManager.Instance.Diamond;

        // UI 업데이트 이벤트 등록
        playerCoin.OnChangedCoinAmountEvent += UpdateCoinUI;
        playerDiamond.OnChangedDiamondAmountEvent += UpdateDiaUI;

        // 초기 UI 표시
        UpdateCoinUI(playerCoin.Current);
        UpdateDiaUI(playerDiamond.Current);
    }

    private void UpdateCoinUI(long amount)
    {
        coinText.text = $" {amount}";
    }

    private void UpdateDiaUI(long amount)
    {
        diaText.text = $" {amount}";
    }

    // 버튼 연결용 메서드
    public void AddCoin(long amount) => playerCoin.Add(amount);
    public void UseCoin(long amount) => playerCoin.Use(amount);
    public void AddDia(long amount) => playerDiamond.Add(amount);
    public void UseDia(long amount) => playerDiamond.Use(amount);
}

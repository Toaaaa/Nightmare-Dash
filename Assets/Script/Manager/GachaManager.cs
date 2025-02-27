using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public Button DrawOneBtn, DrawFiveBtn, ExitBtn;
    public Image GachaFadeBlack;
    public float fadeDuration = 1.0f;
    private float maxAlpha = 0.5f;

    [SerializeField]
    private CardUI[] cards; // 여러 장의 카드 UI 배열

    private Diamond diamond; // 유료 재화

    private readonly int gachaPrice = 100; // 가챠 1회 가격

    private void Start()
    {
        if (cards == null || cards.Length == 0)
        {
            return;
        }

        foreach (var card in cards)
        {
            if (card == null)
            {
                return;
            }
        }

        diamond = DataManager.Instance.Diamond;

        DrawOneBtn.interactable = diamond.IsCanUse(gachaPrice);
        DrawFiveBtn.interactable = diamond.IsCanUse(gachaPrice * 5);

        DrawOneBtn.onClick.AddListener(() => DrawOneBtnClick(1));
        DrawFiveBtn.onClick.AddListener(() => DrawOneBtnClick(5));
        ExitBtn.onClick.AddListener(HideCardAndFadeBlack);
        ExitBtn.gameObject.SetActive(false);
    }

    public void DrawOneBtnClick(int num)
    {
        diamond.Use(gachaPrice * num);
        StartCoroutine(FadeInEffect(num));
    }

    private IEnumerator FadeInEffect(int num)
    {
        GachaFadeBlack.gameObject.SetActive(true);
        GachaFadeBlack.color = new Color(0, 0, 0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        StartCoroutine(ShowCardSlowly(num));
    }

    private IEnumerator ShowCardSlowly(int gachaCount)
    {
        for (int i = 0; i < gachaCount; i++)
        {
            if (i < cards.Length)
            {
                SpawnRandomCard(cards[i]);  // 여러 장의 카드 UI 활용
            }
            yield return new WaitForSeconds(0.1f);
        }

        ExitBtn.gameObject.SetActive(true);
    }

    // 기존 카드 오브젝트를 활용하여 카드 UI 업데이트
    public void SpawnRandomCard(CardUI cardUI)
    {
        if (cardUI == null)
        {
            return;
        }

        cardUI.gameObject.SetActive(true);

        RandomSelect randomSelect = FindObjectOfType<RandomSelect>();
        if (randomSelect == null)
        {
            return;
        }

        Card selectedCard = randomSelect.RandomCard();
        if (selectedCard == null)
        {
            return;
        }

        cardUI.SetCardUI(selectedCard);

        // 뽑은 유물을 플레이어 데이터에 추가
        if (selectedCard.artifact != null)
        {
            OnGachaResult(selectedCard.artifact);
        }
    }

    public void HideCardAndFadeBlack()
    {
        GachaFadeBlack.gameObject.SetActive(false);
        ResetCardState();
        DrawOneBtn.interactable = true;
        DrawFiveBtn.interactable = true;
        ExitBtn.gameObject.SetActive(false);
    }

    public void ResetForNewDraw()
    {
        HideCardAndFadeBlack();
        DrawOneBtn.interactable = false;
        DrawFiveBtn.interactable = false;
        StartCoroutine(FadeInEffect(1));
    }

    private void ResetCardState()
    {
        foreach (var card in cards)
        {
            if (card != null)
            {
                card.gameObject.SetActive(false);
            }
        }
    }

    // DataManager가 로드될 때까지 기다리는 코루틴 추가
    private IEnumerator WaitForDataManagerInitialization(int artifactId)
    {
        while (FindObjectOfType<DataManager>() == null)
        {
            yield return null;
        }

        DataManager dataManager = FindObjectOfType<DataManager>();
        if (dataManager != null)
        {
            dataManager.SetArtifactObtained(artifactId, true);
        }
    }

    // 가챠에서 뽑힌 유물을 플레이어가 획득하도록 적용
    public void OnGachaResult(ArtifactData artifact)
    {
        if (artifact == null)
        {
            return;
        }

        if (GameManager.instance == null || GameManager.instance.playerData == null)
        {
            return;
        }

        // 플레이어에게 유물 추가
        GameManager.instance.playerData.AddArtifact(artifact);

        // DataManager가 초기화될 때까지 기다린 후 실행
        StartCoroutine(WaitForDataManagerInitialization(artifact.Id));
    }
}

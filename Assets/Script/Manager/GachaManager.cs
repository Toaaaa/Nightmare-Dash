using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public Button DrawOneBtn, DrawTenBtn, ExitBtn;
    public Image GachaFadeBlack;
    public float fadeDuration = 1.0f;
    private float maxAlpha = 0.5f;

    [SerializeField]
    private CardUI[] cards; // ✅ 여러 장의 카드 UI 배열

    void Start()
    {
        if (cards == null || cards.Length == 0)
        {
            Debug.LogError("🚨 'cards' 배열이 비어 있습니다! Inspector에서 CardUI 오브젝트들을 연결하세요.");
            return;
        }

        foreach (var card in cards)
        {
            if (card == null)
            {
                Debug.LogError("🚨 'cards' 배열 내에 CardUI가 없는 요소가 있습니다! Inspector에서 확인하세요.");
                return;
            }
        }

        DrawOneBtn.onClick.AddListener(() => DrawOneBtnClick(1));
        DrawTenBtn.onClick.AddListener(() => DrawOneBtnClick(5));
        ExitBtn.onClick.AddListener(HideCardAndFadeBlack);
        ExitBtn.gameObject.SetActive(false);
    }

    public void DrawOneBtnClick(int num)
    {
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
                SpawnRandomCard(cards[i]);  // ✅ 여러 장의 카드 UI 활용
            }
            else
            {
                Debug.LogWarning($"⚠️ {i + 1}번째 카드를 뽑으려고 했으나 'cards' 배열의 크기를 초과했습니다.");
            }
            yield return new WaitForSeconds(0.1f);
        }

        ExitBtn.gameObject.SetActive(true);
    }

    // ✅ 기존 카드 오브젝트를 활용하여 카드 UI 업데이트
    public void SpawnRandomCard(CardUI cardUI)
    {
        if (cardUI == null)
        {
            Debug.LogError("🚨 'CardUI' 오브젝트를 찾을 수 없습니다! 'Card' 오브젝트에 CardUI를 추가하세요.");
            return;
        }

        cardUI.gameObject.SetActive(true);

        RandomSelect randomSelect = FindObjectOfType<RandomSelect>();
        if (randomSelect == null)
        {
            Debug.LogError("🚨 'RandomSelect' 오브젝트를 찾을 수 없습니다! 'Deck' 오브젝트에 RandomSelect 스크립트가 있는지 확인하세요.");
            return;
        }

        Card selectedCard = randomSelect.RandomCard();
        if (selectedCard == null)
        {
            Debug.LogError("🚨 선택된 카드가 null입니다! RandomSelect에서 카드가 올바르게 생성되는지 확인하세요.");
            return;
        }

        // ✅ 카드 데이터 검증 후 UI 적용
        if (string.IsNullOrEmpty(selectedCard.cardName))
        {
            Debug.LogWarning("⚠️ 선택된 카드의 이름이 없습니다.");
            selectedCard.cardName = "Unknown";
        }

        if (string.IsNullOrEmpty(selectedCard.cardType))
        {
            Debug.LogWarning("⚠️ 선택된 카드의 등급 정보가 없습니다.");
            selectedCard.cardType = "Unknown";
        }

        if (selectedCard.cardImage == null)
        {
            Debug.LogWarning($"⚠️ 카드 '{selectedCard.cardName}'의 이미지가 없습니다.");
        }

        Debug.Log($"✅ 랜덤 카드 선택: {selectedCard.cardName} (등급: {selectedCard.cardType})");
        cardUI.SetCardUI(selectedCard);

        // ✅ 뽑은 유물을 플레이어 데이터에 추가
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
        DrawTenBtn.interactable = true;
        ExitBtn.gameObject.SetActive(false);
    }

    public void ResetForNewDraw()
    {
        HideCardAndFadeBlack();
        DrawOneBtn.interactable = false;
        DrawTenBtn.interactable = false;
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

    // ✅ 가챠에서 뽑힌 유물을 플레이어가 획득하도록 적용
    public void OnGachaResult(ArtifactData artifact)
    {
        if (artifact == null)
        {
            Debug.LogError("🚨 뽑힌 유물이 없습니다!");
            return;
        }

        // ✅ 플레이어에게 유물 추가
        PlayerData.Instance.AddArtifact(artifact);

        Debug.Log($"🎁 플레이어가 '{artifact.Name}' 유물을 획득했습니다!");
    }
}

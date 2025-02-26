using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    public Button DrawOneBtn;  // "Draw One" 버튼
    public Button DrawTenBtn;  // "Draw Ten" 버튼
    public Button ExitBtn;     // "Exit" 버튼 (나가기 버튼)
    public Image GachaFadeBlack; // 검은색 페이드 화면
    public float fadeDuration = 1.0f; // 페이드 효과 지속 시간
    private float maxAlpha = 0.5f; // 최대 투명도
    public GameObject card;  // 씬에서 직접 존재하는 카드 오브젝트 (Inspector에서 할당 필요)

    [SerializeField]
    private CardUI[] cards;

    void Start()
    {
        // 버튼 클릭 이벤트 연결
        DrawOneBtn.onClick.AddListener(() => DrawOneBtnClick(1));
        DrawTenBtn.onClick.AddListener(() => DrawOneBtnClick(5));
        ExitBtn.onClick.AddListener(HideCardAndFadeBlack); // 나가기 버튼 이벤트 연결

        // Exit 버튼은 처음에 비활성화
        ExitBtn.gameObject.SetActive(false);

        // 디버깅: 카드가 할당되었는지 확인
        if (card == null)
        {
            Debug.LogError("card is NOT assigned! Assign the card object in the Inspector.");
        }
        else
        {
            card.SetActive(false); // 게임 시작 시 비활성화
            Debug.Log("Card is assigned correctly: " + card.name);
        }
    }

    // 버튼 클릭 시 호출되는 메서드
    public void DrawOneBtnClick(int num)
    {
        StartCoroutine(FadeInEffect(num));
    }

    // Fade in 효과 + 카드 활성화
    private IEnumerator FadeInEffect(int num)
    {
        GachaFadeBlack.gameObject.SetActive(true);
        GachaFadeBlack.color = new Color(0, 0, 0, 0); // 시작은 완전 투명
        float elapsedTime = 0f;

        // 페이드 인 효과 (길게 설정)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 카드 활성화 (카드도 조금 천천히 보이도록 조정)
        if (num == 1)
        {
            if (card != null)
            {
                StartCoroutine(ShowCardSlowly(num));  // 카드 천천히 보이도록 처리
                Debug.Log("Card is now visible: " + card.name);
            }
            else
            {
                Debug.LogError("Card object is null! Make sure it's assigned in the Inspector.");
            }
        }
        else
        {
            StartCoroutine(ShowCardSlowly(num));
        }
    }

    // 카드가 천천히 보이도록 하는 메서드
    private IEnumerator ShowCardSlowly(int gachaCount)
    {
        float cardAlpha = 0f;

        for (int i = 0; i < gachaCount; i++)
        {
            cards[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            cards[i].Flip();
        }

        // 나가기 버튼 활성화
        ExitBtn.gameObject.SetActive(true);
    }

    // 나가기 버튼 클릭 시 카드와 Fade Black 비활성화
    public void HideCardAndFadeBlack()
    {
        GachaFadeBlack.gameObject.SetActive(false); // Fade Black 비활성화
        ResetCardState(); // 카드 비활성화

        // 뽑기 버튼 활성화
        DrawOneBtn.interactable = true;
        DrawTenBtn.interactable = true;

        // Exit 버튼 비활성화
        ExitBtn.gameObject.SetActive(false);
    }

    // 카드 회전 상태 리셋
    private void ResetCardRotation()
    {
        card.transform.rotation = Quaternion.Euler(0, 0, 0); // 카드 회전 초기화
    }

    // 뽑기 버튼을 다시 눌렀을 때 카드와 페이드 블랙을 초기화하여 새로 출력되게 만듦
    public void ResetForNewDraw()
    {
        HideCardAndFadeBlack(); // 기존 카드와 Fade Black 비활성화

        // 카드가 새로 뽑힐 때 다시 활성화되고 초기 상태로 설정됨
        DrawOneBtn.interactable = false; // 뽑기 버튼 비활성화
        DrawTenBtn.interactable = false;

        // 카드 회전 상태 초기화
        ResetCardRotation(); // 카드 회전 리셋
        card.SetActive(true); // 카드 활성화

        // 새로운 뽑기를 시작
        StartCoroutine(FadeInEffect(1));  // 새로 뽑기 시작
    }

    private void ResetCardState()
    {
        foreach (var card in cards)
        {
            card.gameObject.SetActive(false);
        }
    }
}

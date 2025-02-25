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

    void Start()
    {
        // 버튼 클릭 이벤트 연결
        DrawOneBtn.onClick.AddListener(() => DrawOneBtnClick(1));
        DrawTenBtn.onClick.AddListener(() => DrawOneBtnClick(10));
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
        // 카드와 Fade Black 초기화
        ResetCardRotation(); // 카드 회전 리셋

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
                StartCoroutine(ShowCardSlowly());  // 카드 천천히 보이도록 처리
                Debug.Log("Card is now visible: " + card.name);
            }
            else
            {
                Debug.LogError("Card object is null! Make sure it's assigned in the Inspector.");
            }
        }
        else
        {
            Debug.Log("10개 뽑기 로직이 필요함!");
        }
    }

    // 카드가 천천히 보이도록 하는 메서드
    private IEnumerator ShowCardSlowly()
    {
        float cardAlpha = 0f;
        card.SetActive(true); // 카드 활성화

        // 카드가 점진적으로 보이도록 처리
        CanvasGroup cardCanvasGroup = card.GetComponent<CanvasGroup>();
        if (cardCanvasGroup == null)
        {
            cardCanvasGroup = card.AddComponent<CanvasGroup>();  // CanvasGroup이 없으면 추가
        }

        // 카드의 알파 값을 점진적으로 증가시켜서 보이게 만듦
        while (cardAlpha < 1f)
        {
            cardAlpha += Time.deltaTime / fadeDuration; // fadeDuration에 맞춰 천천히 변화
            cardCanvasGroup.alpha = Mathf.Lerp(0, 1, cardAlpha);
            yield return null;
        }

        cardCanvasGroup.alpha = 1f;  // 최종적으로 완전하게 보이게 설정

        // 카드 뒤집기 효과 추가 (회전 효과)
        StartCoroutine(CardFlipEffect());

        // 나가기 버튼 활성화
        ExitBtn.gameObject.SetActive(true);
    }

    // 카드가 뒤집어지는 효과
    private IEnumerator CardFlipEffect()
    {
        float rotationAmount = 0f;
        Quaternion startRotation = card.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 180, 0); // 180도 회전

        // 카드가 천천히 회전하도록 처리
        while (rotationAmount < 1f)
        {
            rotationAmount += Time.deltaTime / fadeDuration;
            card.transform.rotation = Quaternion.Slerp(startRotation, endRotation, rotationAmount);
            yield return null;
        }

        card.transform.rotation = endRotation; // 최종적으로 완전 회전
    }

    // 나가기 버튼 클릭 시 카드와 Fade Black 비활성화
    public void HideCardAndFadeBlack()
    {
        GachaFadeBlack.gameObject.SetActive(false); // Fade Black 비활성화
        card.SetActive(false); // 카드 비활성화

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
}

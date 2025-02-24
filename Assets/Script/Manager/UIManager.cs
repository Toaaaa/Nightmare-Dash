using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button DrawOneBtn;  // "Draw One" 버튼
    public Button DrawTenBtn;  // "Draw Ten" 버튼
    public Image GachaFadeBlack; // 검은색 페이드 화면
    public float fadeDuration = 0.5f; // 페이드 효과 지속 시간
    private float maxAlpha = 0.5f; // 최대 투명도 (0 = 완전 투명, 1 = 완전 불투명)

    private void Start()
    {
        // 버튼 클릭 시 Gacha 연출 실행
        DrawOneBtn.onClick.AddListener(() => StartCoroutine(GachaFadeEffect()));
        DrawTenBtn.onClick.AddListener(() => StartCoroutine(GachaFadeEffect()));
    }

    // 페이드 인/아웃 효과 (반투명 유지)
    private IEnumerator GachaFadeEffect()
    {
        // 1. 화면을 반투명하게 만듦 (페이드 인)
        GachaFadeBlack.gameObject.SetActive(true);
        GachaFadeBlack.color = new Color(0, 0, 0, 0); // 투명하게 시작
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 2. 가챠 진행 중 (반투명 상태 유지)
        GachaFadeBlack.color = new Color(0, 0, 0, maxAlpha);
        yield return new WaitForSeconds(1.0f); // 가챠 진행 시간 (1초 동안 유지)

        // 3. 화면을 다시 투명하게 만들기 (페이드 아웃)
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(maxAlpha, 0, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        GachaFadeBlack.gameObject.SetActive(false); // 페이드 아웃 후 비활성화
    }
}

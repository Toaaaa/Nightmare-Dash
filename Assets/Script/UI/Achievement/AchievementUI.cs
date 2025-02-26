using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public TMP_Text achievementText;
    public CanvasGroup achievementPanel; // UI의 알파값 조정
    public float displayTime = 3f; // 업적 표시 시간

    private void Start()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementUnlocked += ShowAchievement; // 이벤트 구독
            achievementPanel.alpha = 0; // UI 숨기기
        }
        else
        {
            Debug.LogError("AchievementManager가 초기화되지 않았습니다!");
        }
    }

    public void ShowAchievement(string achievementName)
    {
        StopAllCoroutines(); // 이전 효과 중지
        achievementText.text = $"{achievementName} 달성!";
        StartCoroutine(DisplayAchievement());
    }

    private IEnumerator DisplayAchievement()
    {
        achievementPanel.alpha = 1; // UI 표시
        yield return new WaitForSeconds(displayTime);
        achievementPanel.alpha = 0; // UI 숨기기
    }
}

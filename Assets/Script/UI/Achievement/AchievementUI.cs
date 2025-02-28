using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public TMP_Text achievementText;
    public CanvasGroup achievementPanel; // UI의 알파값 조정
    public float displayTime = 3f; // 업적 표시 시간
    private HashSet<string> displayedAchievements = new HashSet<string>(); // 이미 표시된 업적 저장

    private void Start()
    {
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementUnlocked += ShowAchievement; // 이벤트 구독
            achievementPanel.alpha = 0; // UI 숨기기
            LoadDisplayedAchievements(); // 🔹 표시된 업적 불러오기
        }
        else
        {
            Debug.LogError("AchievementManager가 초기화되지 않았습니다!");
        }
    }

    private void LoadDisplayedAchievements()
    {
        foreach (var achievement in AchievementManager.Instance.achievements)
        {
            if (achievement.IsUnlocked)
            {
                displayedAchievements.Add(achievement.Name); // 🔹 해금된 업적을 표시된 목록에 추가
            }
        }
    }

    public void ShowAchievement(string achievementName)
    {
        // 이미 해금된 업적이면 표시하지 않음
        if (displayedAchievements.Contains(achievementName))
            return;

        displayedAchievements.Add(achievementName); // 업적을 기록
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

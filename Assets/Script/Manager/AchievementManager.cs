using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }
    public List<Achievement> achievements = new List<Achievement>();
    public event Action<string> OnAchievementUnlocked; // 업적 해금 이벤트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeAchievements();
        LoadAchievements();
    }

    private void InitializeAchievements()
    {
        achievements = new List<Achievement>
        {
            new Achievement { Name = "첫 걸음", IsUnlocked = false , Description = "튜토리얼 1회 경험하기", Reward = 100 },
            new Achievement { Name = "공포 속으로", IsUnlocked = false, Description = "귀신과 마주치기", Reward = 100 },
            new Achievement {Name = "빛이 사라졌다", IsUnlocked = false, Description = "조명 꺼짐 겪어보기", Reward = 50},
            new Achievement {Name = "재산 확인", IsUnlocked = false, Description = "최초로 재화 획득하기", Reward = 50},
            new Achievement {Name = "비밀을 발견하다", IsUnlocked = false, Description = "히든 리소스 찾기", Reward = 1000},
            new Achievement {Name = "망가진 현실", IsUnlocked = false, Description = "귀신 5번 마주하기", Reward = 100},
            new Achievement {Name = "뒤를 돌아보지 마", IsUnlocked = false, Description = "게임 클리어 하기", Reward = 1000},
            new Achievement {Name = "첫 번째 재산", IsUnlocked = false, Description = "재화를 다이아로 교환하기", Reward = 50},
            new Achievement {Name = "부자가 되는 길", IsUnlocked = false, Description = "다이아 1000개 모으기", Reward = 100},
            new Achievement {Name = "돈을 써야 돈이 돈다", IsUnlocked = false, Description = "가챠 5회 돌리기", Reward = 100},
            new Achievement {Name = "탈출을 꿈꾸다", IsUnlocked = false, Description = "30초 동안 피격 없이 달리기", Reward = 100}
        };
    }

    public void UnlockAchievement(string name)
    {
        Achievement ach = achievements.Find(a => a.Name == name);
        if (ach != null && !ach.IsUnlocked)
        {
            ach.IsUnlocked = true;
            OnAchievementUnlocked?.Invoke(ach.Name); // UI 업데이트 호출
            Debug.Log($" 업적 달성: {name}");
            DataManager.Instance.Diamond.Add(ach.Reward);
            SaveAchievements();
        }
    }

    // 🔹 업적 저장
    private void SaveAchievements()
    {
        foreach (var achievement in achievements)
        {
            PlayerPrefs.SetInt($"Achievement_{achievement.Name}", achievement.IsUnlocked ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    // 🔹 업적 불러오기
    private void LoadAchievements()
    {
        foreach (var achievement in achievements)
        {
            if (PlayerPrefs.HasKey($"Achievement_{achievement.Name}"))
            {
                achievement.IsUnlocked = PlayerPrefs.GetInt($"Achievement_{achievement.Name}") == 1;
            }
        }
    }
}
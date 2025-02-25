using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Achievement
{
    public string Name;
    public string Description;
    public bool IsUnlocked;
}

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;//싱글톤
    public List<Achievement> achievements = new List<Achievement>();//업적리스트

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
        }
    }

    public void UnlockAchievement(string name)
    {
        Achievement ach = achievements.Find(a => a.Name == name);
        if (ach != null && !ach.IsUnlocked)
        {
            ach.IsUnlocked = true;
            Debug.Log($"업적 달성: {name}");
        }
    }
    public bool IsAchievementUnlocked(string name)
    {
        Achievement ach = achievements.Find(a => a.Name == name);
        return ach != null && ach.IsUnlocked;
    }

    private void Start()
    {
        achievements = new List<Achievement>
    {
        new Achievement { Name = "첫 걸음", IsUnlocked = false },
        new Achievement { Name = "공포 속으로", IsUnlocked = false },
        new Achievement { Name = "빛이 사라졌다", IsUnlocked = false },
        new Achievement { Name = "재산 확인", IsUnlocked = false },
        new Achievement { Name = "비밀을 발견하다", IsUnlocked = false },
        new Achievement { Name = "망가진 현실", IsUnlocked = false },
        new Achievement { Name = "뒤를 돌아보지 마", IsUnlocked = false },
        new Achievement { Name = "첫 번째 재산", IsUnlocked = false },
        new Achievement { Name = "부자가 되는 길", IsUnlocked = false },
        new Achievement { Name = "돈을 써야 돈이 돈다", IsUnlocked = false },
        new Achievement { Name = "탈출을 꿈꾸다", IsUnlocked = false }
    };
    }
}


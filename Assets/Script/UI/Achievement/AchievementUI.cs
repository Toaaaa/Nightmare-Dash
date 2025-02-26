using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    public TMP_Text achievementText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        achievementText.text = "";
        foreach (var ach in AchievementManager.Instance.achievements)
        {
            achievementText.text += ach.IsUnlocked ? $"{ach.Name}\n" : $"{ach.Name}\n";
        }
    }
}

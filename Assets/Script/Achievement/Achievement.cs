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

    public void Unlock()
    {
        if (!IsUnlocked)
        {
            IsUnlocked = true;
            Debug.Log($"업적 달성: {Name} - {Description}");
        }
    }
}



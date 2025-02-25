using System;
using System.Collections.Generic;
using UnityEngine;

// 유물 등급을 정의하는 열거형
public enum ArtifactRarity
{
    C,
    B,
    A,
    S
}

[System.Serializable]
public class Effect
{
    public float Hp { get; set; }
    public float Currency { get; set; }
    public float Invincibility { get; set; }
}

[System.Serializable]
public class ArtifactData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ArtifactRarity Rarity { get; set; }  // 문자열 대신 열거형 사용
    public Effect Effect { get; set; }
    public bool IsObtained { get; set; }     // 유물을 얻었는지 여부
}

public class Artifacts : MonoBehaviour
{
    // 유물 데이터를 저장하는 리스트
    public List<ArtifactData> ArtifactsList { get; set; } = new List<ArtifactData>();

    void Start()
    {
        // 유물 데이터 초기화
        InitializeArtifacts();

        // 유물 출력 (디버깅용)
        foreach (var artifact in ArtifactsList)
        {
            Debug.Log($"Artifact ID: {artifact.Id}, Name: {artifact.Name}, Rarity: {artifact.Rarity}, Obtained: {artifact.IsObtained}");
        }
    }

    // 유물 데이터 초기화 메서드
    void InitializeArtifacts()
    {
        ArtifactsList.Add(new ArtifactData
        {
            Id = 1,
            Name = "체력의 돌 C",
            Rarity = ArtifactRarity.C,
            Effect = new Effect { Hp = 0.5f, Currency = 0.0f, Invincibility = 0.2f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 2,
            Name = "재화의 부적 C",
            Rarity = ArtifactRarity.C,
            Effect = new Effect { Hp = 0.0f, Currency = 1.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 3,
            Name = "무적의 부적 C",
            Rarity = ArtifactRarity.C,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.2f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 4,
            Name = "체력의 돌 B",
            Rarity = ArtifactRarity.B,
            Effect = new Effect { Hp = 1.0f, Currency = 0.0f, Invincibility = 0.5f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 5,
            Name = "재화의 부적 B",
            Rarity = ArtifactRarity.B,
            Effect = new Effect { Hp = 0.0f, Currency = 2.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 6,
            Name = "무적의 부적 B",
            Rarity = ArtifactRarity.B,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.5f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 7,
            Name = "체력의 돌 A",
            Rarity = ArtifactRarity.A,
            Effect = new Effect { Hp = 2.5f, Currency = 0.0f, Invincibility = 1.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 8,
            Name = "재화의 부적 A",
            Rarity = ArtifactRarity.A,
            Effect = new Effect { Hp = 0.0f, Currency = 4.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 9,
            Name = "무적의 부적 A",
            Rarity = ArtifactRarity.A,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 1.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 10,
            Name = "체력의 돌 S",
            Rarity = ArtifactRarity.S,
            Effect = new Effect { Hp = 5.0f, Currency = 0.0f, Invincibility = 2.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 11,
            Name = "재화의 부적 S",
            Rarity = ArtifactRarity.S,
            Effect = new Effect { Hp = 0.0f, Currency = 8.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new ArtifactData
        {
            Id = 12,
            Name = "무적의 부적 S",
            Rarity = ArtifactRarity.S,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 2.0f },
            IsObtained = false
        });
    }

    // 랜덤으로 유물 하나를 뽑는 메서드
    public ArtifactData GetRandomArtifact()
    {
        if (ArtifactsList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, ArtifactsList.Count);
            return ArtifactsList[randomIndex];
        }
        return null;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

// 유물 등급을 열거형으로 정의
public enum RarityType
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
public class Artifact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RarityType Rarity { get; set; } // string -> 열거형으로 변경
    public Effect Effect { get; set; }
    public bool IsObtained { get; set; }
}

public class Artifacts : MonoBehaviour
{
    public List<Artifact> ArtifactsList { get; set; } = new List<Artifact>();

    void Start()
    {
        InitializeArtifacts();

        foreach (var artifact in ArtifactsList)
        {
            Debug.Log($"Artifact ID: {artifact.Id}, Name: {artifact.Name}, Rarity: {artifact.Rarity}, Obtained: {artifact.IsObtained}");
        }
    }

    void InitializeArtifacts()
    {
        ArtifactsList.Add(new Artifact
        {
            Id = 1,
            Name = "체력의 돌 C",
            Rarity = RarityType.C,
            Effect = new Effect { Hp = 0.5f, Currency = 0.0f, Invincibility = 0.2f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 2,
            Name = "재화의 부적 C",
            Rarity = RarityType.C,
            Effect = new Effect { Hp = 0.0f, Currency = 1.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 3,
            Name = "무적의 부적 C",
            Rarity = RarityType.C,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.2f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 4,
            Name = "체력의 돌 B",
            Rarity = RarityType.B,
            Effect = new Effect { Hp = 1.0f, Currency = 0.0f, Invincibility = 0.5f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 5,
            Name = "재화의 부적 B",
            Rarity = RarityType.B,
            Effect = new Effect { Hp = 0.0f, Currency = 2.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 6,
            Name = "무적의 부적 B",
            Rarity = RarityType.B,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.5f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 7,
            Name = "체력의 돌 A",
            Rarity = RarityType.A,
            Effect = new Effect { Hp = 2.5f, Currency = 0.0f, Invincibility = 1.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 8,
            Name = "재화의 부적 A",
            Rarity = RarityType.A,
            Effect = new Effect { Hp = 0.0f, Currency = 4.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 9,
            Name = "무적의 부적 A",
            Rarity = RarityType.A,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 1.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 10,
            Name = "체력의 돌 S",
            Rarity = RarityType.S,
            Effect = new Effect { Hp = 5.0f, Currency = 0.0f, Invincibility = 2.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 11,
            Name = "재화의 부적 S",
            Rarity = RarityType.S,
            Effect = new Effect { Hp = 0.0f, Currency = 8.0f, Invincibility = 0.0f },
            IsObtained = false
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 12,
            Name = "무적의 부적 S",
            Rarity = RarityType.S,
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 2.0f },
            IsObtained = false
        });
    }

    public Artifact GetRandomArtifact()
    {
        if (ArtifactsList.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, ArtifactsList.Count);
            return ArtifactsList[randomIndex];
        }
        return null;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public float Hp { get; set; }            // 자동 구현 프로퍼티로 수정
    public float Currency { get; set; }     
    public float Invincibility { get; set; } 
}

[System.Serializable]
public class Artifact
{
    public int Id { get; set; }              // 자동 구현 프로퍼티로 수정
    public string Name { get; set; }         
    public string Rarity { get; set; }     
    public Effect Effect { get; set; }       
}

public class Artifacts : MonoBehaviour
{
    // 유물 데이터를 직접 코드 내에서 정의
    public List<Artifact> ArtifactsList { get; set; } = new List<Artifact>(); 

    void Start()
    {
        // 유물 데이터 초기화
        InitializeArtifacts();

        // 유물 출력 (디버깅용)
        foreach (var artifact in ArtifactsList)
        {
            Debug.Log($"Artifact ID: {artifact.Id}, Name: {artifact.Name}, Rarity: {artifact.Rarity}");
        }
    }

    // 유물 데이터 초기화 메서드
    void InitializeArtifacts()
    {
        ArtifactsList.Add(new Artifact
        {
            Id = 1,
            Name = "체력의 돌 C",
            Rarity = "C",
            Effect = new Effect { Hp = 0.5f, Currency = 0.0f, Invincibility = 0.2f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 2,
            Name = "재화의 부적 C",
            Rarity = "C",
            Effect = new Effect { Hp = 0.0f, Currency = 1.0f, Invincibility = 0.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 3,
            Name = "무적의 부적 C",
            Rarity = "C",
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.2f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 4,
            Name = "체력의 돌 B",
            Rarity = "B",
            Effect = new Effect { Hp = 1.0f, Currency = 0.0f, Invincibility = 0.5f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 5,
            Name = "재화의 부적 B",
            Rarity = "B",
            Effect = new Effect { Hp = 0.0f, Currency = 2.0f, Invincibility = 0.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 6,
            Name = "무적의 부적 B",
            Rarity = "B",
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.5f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 7,
            Name = "체력의 돌 A",
            Rarity = "A",
            Effect = new Effect { Hp = 2.5f, Currency = 0.0f, Invincibility = 1.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 8,
            Name = "재화의 부적 A",
            Rarity = "A",
            Effect = new Effect { Hp = 0.0f, Currency = 4.0f, Invincibility = 0.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 9,
            Name = "무적의 부적 A",
            Rarity = "A",
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 1.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 10,
            Name = "체력의 돌 S",
            Rarity = "S",
            Effect = new Effect { Hp = 5.0f, Currency = 0.0f, Invincibility = 2.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 11,
            Name = "재화의 부적 S",
            Rarity = "S",
            Effect = new Effect { Hp = 0.0f, Currency = 8.0f, Invincibility = 0.0f }
        });

        ArtifactsList.Add(new Artifact
        {
            Id = 12,
            Name = "무적의 부적 S",
            Rarity = "S",
            Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 2.0f }
        });
    }

    // 랜덤으로 유물 하나를 뽑는 메서드
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

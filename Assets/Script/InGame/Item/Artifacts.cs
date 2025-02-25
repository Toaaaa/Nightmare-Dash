using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public float hp;
    public float currency;
    public float invincibility;
}

[System.Serializable]
public class Artifact
{
    public string id;
    public string name;
    public string rarity;
    public Effect effect;
}

public class Artifacts : MonoBehaviour
{
    // 유물 데이터를 직접 코드 내에서 정의
    public List<Artifact> artifacts = new List<Artifact>();

    void Start()
    {
        // 유물 데이터 초기화
        InitializeArtifacts();

        // 유물 출력 (디버깅용)
        foreach (var artifact in artifacts)
        {
            Debug.Log($"Artifact Name: {artifact.name}, Rarity: {artifact.rarity}");
        }
    }

    // 유물 데이터 초기화 메서드
    void InitializeArtifacts()
    {
        artifacts.Add(new Artifact
        {
            id = "artifact_001",
            name = "체력의 돌 C",
            rarity = "C",
            effect = new Effect { hp = 0.5f, currency = 0.0f, invincibility = 0.2f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_002",
            name = "재화의 부적 C",
            rarity = "C",
            effect = new Effect { hp = 0.0f, currency = 1.0f, invincibility = 0.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_003",
            name = "무적의 부적 C",
            rarity = "C",
            effect = new Effect { hp = 0.0f, currency = 0.0f, invincibility = 0.2f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_004",
            name = "체력의 돌 B",
            rarity = "B",
            effect = new Effect { hp = 1.0f, currency = 0.0f, invincibility = 0.5f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_005",
            name = "재화의 부적 B",
            rarity = "B",
            effect = new Effect { hp = 0.0f, currency = 2.0f, invincibility = 0.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_006",
            name = "무적의 부적 B",
            rarity = "B",
            effect = new Effect { hp = 0.0f, currency = 0.0f, invincibility = 0.5f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_007",
            name = "체력의 돌 A",
            rarity = "A",
            effect = new Effect { hp = 2.5f, currency = 0.0f, invincibility = 1.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_008",
            name = "재화의 부적 A",
            rarity = "A",
            effect = new Effect { hp = 0.0f, currency = 4.0f, invincibility = 0.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_009",
            name = "무적의 부적 A",
            rarity = "A",
            effect = new Effect { hp = 0.0f, currency = 0.0f, invincibility = 1.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_010",
            name = "체력의 돌 S",
            rarity = "S",
            effect = new Effect { hp = 5.0f, currency = 0.0f, invincibility = 2.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_011",
            name = "재화의 부적 S",
            rarity = "S",
            effect = new Effect { hp = 0.0f, currency = 8.0f, invincibility = 0.0f }
        });

        artifacts.Add(new Artifact
        {
            id = "artifact_012",
            name = "무적의 부적 S",
            rarity = "S",
            effect = new Effect { hp = 0.0f, currency = 0.0f, invincibility = 2.0f }
        });
    }

    // 랜덤으로 유물 하나를 뽑는 메서드
    public Artifact GetRandomArtifact()
    {
        if (artifacts.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, artifacts.Count);
            return artifacts[randomIndex];
        }
        return null;
    }
}

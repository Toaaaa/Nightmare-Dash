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
    public Sprite ArtifactImage { get; set; }  // ✅ 유물 이미지 추가
}

public class Artifacts : MonoBehaviour
{
    public List<ArtifactData> ArtifactsList { get; set; } = new List<ArtifactData>();
    public Sprite[] ArtifactImages;  // ✅ 유물 이미지를 저장할 배열

    void Start()
    {
        // ✅ Resources에서 유물 이미지 불러오기
        ArtifactImages = Resources.LoadAll<Sprite>("Artifacts");

        // ✅ 이미지 이름 기준으로 정렬 (Artifacts_1, Artifacts_2 순서대로)
        Array.Sort(ArtifactImages, (a, b) => a.name.CompareTo(b.name));

        Debug.Log($"📂 로드된 유물 이미지 개수: {ArtifactImages.Length}");

        // ✅ 유물 데이터 초기화
        InitializeArtifacts();

        // ✅ 유물 출력 (디버깅용)
        foreach (var artifact in ArtifactsList)
        {
            Debug.Log($"Artifact ID: {artifact.Id}, Name: {artifact.Name}, Rarity: {artifact.Rarity}, Obtained: {artifact.IsObtained}, Image: {artifact.ArtifactImage?.name}");
        }
    }

    // ✅ 유물 데이터 초기화 메서드
    void InitializeArtifacts()
    {
        ArtifactsList.Add(new ArtifactData { Id = 1, Name = "체력의 돌 C", Rarity = ArtifactRarity.C, Effect = new Effect { Hp = 0.5f, Currency = 0.0f, Invincibility = 0.2f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 2, Name = "재화의 부적 C", Rarity = ArtifactRarity.C, Effect = new Effect { Hp = 0.0f, Currency = 1.0f, Invincibility = 0.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 3, Name = "무적의 부적 C", Rarity = ArtifactRarity.C, Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.2f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 4, Name = "체력의 돌 B", Rarity = ArtifactRarity.B, Effect = new Effect { Hp = 1.0f, Currency = 0.0f, Invincibility = 0.5f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 5, Name = "재화의 부적 B", Rarity = ArtifactRarity.B, Effect = new Effect { Hp = 0.0f, Currency = 2.0f, Invincibility = 0.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 6, Name = "무적의 부적 B", Rarity = ArtifactRarity.B, Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 0.5f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 7, Name = "체력의 돌 A", Rarity = ArtifactRarity.A, Effect = new Effect { Hp = 2.5f, Currency = 0.0f, Invincibility = 1.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 8, Name = "재화의 부적 A", Rarity = ArtifactRarity.A, Effect = new Effect { Hp = 0.0f, Currency = 4.0f, Invincibility = 0.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 9, Name = "무적의 부적 A", Rarity = ArtifactRarity.A, Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 1.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 10, Name = "체력의 돌 S", Rarity = ArtifactRarity.S, Effect = new Effect { Hp = 5.0f, Currency = 0.0f, Invincibility = 2.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 11, Name = "재화의 부적 S", Rarity = ArtifactRarity.S, Effect = new Effect { Hp = 0.0f, Currency = 8.0f, Invincibility = 0.0f }, IsObtained = false });
        ArtifactsList.Add(new ArtifactData { Id = 12, Name = "무적의 부적 S", Rarity = ArtifactRarity.S, Effect = new Effect { Hp = 0.0f, Currency = 0.0f, Invincibility = 2.0f }, IsObtained = false });

        // ✅ 이미지 파일 이름과 유물 ID를 매칭
        for (int i = 0; i < ArtifactsList.Count; i++)
        {
            string expectedImageName = $"Artifacts_{ArtifactsList[i].Id}"; // ✅ 기대하는 이미지 이름
            Sprite matchedSprite = Array.Find(ArtifactImages, img => img.name == expectedImageName);

            if (matchedSprite != null)
            {
                ArtifactsList[i].ArtifactImage = matchedSprite;
            }
            else
            {
                Debug.LogWarning($"⚠️ ArtifactsList[{i}] ({ArtifactsList[i].Name})에 대한 이미지 ({expectedImageName})를 찾을 수 없습니다.");
                ArtifactsList[i].ArtifactImage = null;
            }
        }

        Debug.Log($"✅ 유물 데이터 초기화 완료! 총 유물 개수: {ArtifactsList.Count}");
    }

    // ✅ 랜덤으로 유물 하나를 뽑는 메서드
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    void Start()
    {
        // ✅ Artifacts.cs에서 유물 데이터 가져오기
        Artifacts artifactManager = FindObjectOfType<Artifacts>();

        if (artifactManager == null)
        {
            Debug.LogError("🚨 Artifacts Manager를 찾을 수 없습니다! 'Artifacts' 스크립트가 있는지 확인하세요.");
            return;
        }

        // ✅ Artifacts.cs의 데이터를 기반으로 덱 구성
        foreach (var artifact in artifactManager.ArtifactsList)
        {
            if (artifact == null)
            {
                Debug.LogError("🚨 artifact가 null입니다! ArtifactsList를 확인하세요.");
                continue;
            }

            // ✅ 유물 효과가 null일 경우 기본값 설정
            if (artifact.Effect == null)
            {
                Debug.LogWarning($"⚠️ 유물 '{artifact.Name}'의 효과 데이터가 없습니다. 기본값(0)으로 설정합니다.");
                artifact.Effect = new Effect { Hp = 0, Currency = 0, Invincibility = 0 };
            }

            Card card = new Card
            {
                cardName = artifact.Name ?? "Unknown",
                cardType = artifact.Rarity.ToString(),
                cardEffect = $"HP: {artifact.Effect.Hp}, Currency: {artifact.Effect.Currency}, Invincibility: {artifact.Effect.Invincibility}",
                cardImage = artifact.ArtifactImage,
                artifact = artifact,
                weight = GetWeightByRarity(artifact.Rarity) // ✅ 유물 등급에 따라 가중치 적용
            };

            deck.Add(card);
            total += card.weight;
        }

        if (deck.Count == 0 || total == 0)
        {
            Debug.LogError("🚨 덱이 비어 있거나 가중치 합이 0입니다. 유물 데이터가 정상적으로 로드되었는지 확인하세요.");
        }
        else
        {
            Debug.Log($"✅ 덱 초기화 완료! 총 카드 개수: {deck.Count}, 가중치 총합: {total}");
        }
    }

    // ✅ 유물 등급에 따라 가중치 반환
    private int GetWeightByRarity(ArtifactRarity rarity)
    {
        switch (rarity)
        {
            case ArtifactRarity.S: return 5;  // S등급 (가장 희귀)
            case ArtifactRarity.A: return 10; // A등급
            case ArtifactRarity.B: return 15; // B등급
            case ArtifactRarity.C: return 20; // C등급 (가장 흔함)
            default: return 10;
        }
    }

    // ✅ 가중치를 기반으로 랜덤 카드 선택 (GachaManager에서 호출됨)
    public Card RandomCard()
    {
        if (deck == null || deck.Count == 0 || total == 0)
        {
            Debug.LogError("🚨 덱에 카드가 없거나 가중치가 0입니다. Artifacts.cs를 확인하세요.");
            return null;
        }

        int randomValue = Random.Range(0, total);
        int accumulatedWeight = 0;

        foreach (var card in deck)
        {
            accumulatedWeight += card.weight;
            if (randomValue <= accumulatedWeight)
            {
                if (card == null)
                {
                    Debug.LogError("🚨 선택된 카드가 null입니다. 덱을 확인하세요.");
                    return null;
                }

                Card selectedCard = new Card(card);
                Debug.Log($"🎲 랜덤 카드 선택: {selectedCard.cardName} (등급: {selectedCard.cardType})");

                // ✅ 카드 유물 정보 확인
                if (selectedCard.artifact == null)
                {
                    Debug.LogWarning($"⚠️ 카드 '{selectedCard.cardName}'에 연결된 유물이 없습니다.");
                }
                else if (selectedCard.artifact.ArtifactImage == null)
                {
                    Debug.LogWarning($"⚠️ 카드 '{selectedCard.cardName}'에 연결된 유물 '{selectedCard.artifact.Name}'의 이미지가 없습니다.");
                }

                return selectedCard;
            }
        }

        Debug.LogError("🚨 가중치를 기반으로 카드 선택에 실패했습니다.");
        return null;
    }
}

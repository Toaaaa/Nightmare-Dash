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
            return;
        }

        // ✅ ArtifactsList가 비어 있으면 다시 가져오기 시도
        if (artifactManager.ArtifactsList.Count == 0)
        {
            StartCoroutine(WaitAndInitializeDeck(1f, artifactManager));
            return;
        }

        InitializeDeck(artifactManager);
    }

    // ✅ ArtifactsList가 비어 있을 경우 다시 시도하는 코루틴
    private IEnumerator WaitAndInitializeDeck(float delay, Artifacts artifactManager)
    {
        yield return new WaitForSeconds(delay);

        if (artifactManager.ArtifactsList.Count == 0)
        {
            yield break;
        }

        InitializeDeck(artifactManager);
    }

    // ✅ 덱 초기화 메서드
    private void InitializeDeck(Artifacts artifactManager)
    {
        foreach (var artifact in artifactManager.ArtifactsList)
        {
            if (artifact == null)
            {
                continue;
            }

            Card card = new Card
            {
                cardName = artifact.Name ?? "Unknown",
                cardType = artifact.Rarity.ToString(),
                cardEffect = artifact.GetEffectDescription(), // ✅ 효과 설명 한글로 표시
                cardImage = artifact.ArtifactImage,
                artifact = artifact,
                weight = GetWeightByRarity(artifact.Rarity)
            };

            deck.Add(card);
            total += card.weight;
        }

        if (deck.Count == 0 || total == 0)
        {
            return;
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
                    return null;
                }

                Card selectedCard = new Card(card);

                // ✅ 카드 유물 정보 확인
                if (selectedCard.artifact == null)
                {
                    // 경고 없이 유물 없음 처리
                }
                else if (selectedCard.artifact.ArtifactImage == null)
                {
                    // 경고 없이 유물 이미지 없음 처리
                }

                return selectedCard;
            }
        }
        return null;
    }
}

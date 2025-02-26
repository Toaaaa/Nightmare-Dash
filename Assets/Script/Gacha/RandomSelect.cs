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

        // ✅ ArtifactsList가 비어 있으면 다시 가져오기 시도
        if (artifactManager.ArtifactsList.Count == 0)
        {
            Debug.LogWarning("⚠️ ArtifactsList가 비어 있습니다. 1초 후 다시 시도합니다.");
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
            Debug.LogError("🚨 ArtifactsList가 여전히 비어 있습니다. 덱을 생성할 수 없습니다.");
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
                Debug.LogError("🚨 artifact가 null입니다! ArtifactsList를 확인하세요.");
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

            Debug.Log($"✅ 카드 추가됨: {card.cardName}, 효과: {card.cardEffect}, 가중치: {card.weight}");
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

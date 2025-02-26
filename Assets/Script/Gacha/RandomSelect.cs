using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    [System.Serializable]
    public class Artifact
    {
        public string cardName;
        public string cardType;
        public int weight;
        public string cardEffect;
        public Sprite cardImage;
    }

    [System.Serializable]
    public class ArtifactList
    {
        public List<Artifact> artifacts;
    }

    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    public Transform cardSpawnPoint;  // 카드가 생성될 위치
    public GameObject cardPrefab;  // 카드 프리팹

    void Start()
    {
        // JSON 파일 불러오기
        TextAsset jsonData = Resources.Load<TextAsset>("artifacts");  // artifacts.json 파일
        if (jsonData != null)
        {
            ArtifactList artifactList = JsonUtility.FromJson<ArtifactList>(jsonData.ToString());
            foreach (var artifact in artifactList.artifacts)
            {
                // Artifact를 Card로 변환하여 덱에 추가
                Card card = new Card
                {
                    cardName = artifact.cardName,
                    cardType = artifact.cardType,
                    cardEffect = artifact.cardEffect,
                    cardImage = artifact.cardImage
                };

                // 카드 등급에 따라 weight 값 설정
                switch (card.cardType)  // 예시: cardType에 따라 weight를 설정
                {
                    case "S":
                        card.weight = 5; // 낮은 가중치 (잘 안 나옴)
                        break;
                    case "A":
                        card.weight = 10; // 중간 가중치
                        break;
                    case "B":
                        card.weight = 15; // 높은 가중치
                        break;
                    case "C":
                        card.weight = 20; // 가장 높은 가중치 (잘 나옴)
                        break;
                    default:
                        card.weight = 10; // 기본 값
                        break;
                }

                deck.Add(card);  // 덱에 Card 객체 추가
            }

            // 덱에 있는 카드들의 가중치 총합 계산
            for (int i = 0; i < deck.Count; i++)
            {
                total += deck[i].weight;
            }
        }
        else
        {
            Debug.LogError("JSON 파일을 찾을 수 없습니다.");
        }
    }


    // 가중치 랜덤으로 카드를 선택하는 메서드
    public Card RandomCard()
    {
        int weight = 0;
        int selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                return temp;
            }
        }
        return null;
    }

    // 카드를 랜덤으로 뽑아 화면에 표시하는 메서드
    public void SpawnRandomCard()
    {
        Card selectedCard = RandomCard();
        if (selectedCard != null)
        {
            // 카드 생성
            GameObject cardObject = Instantiate(cardPrefab, cardSpawnPoint);
            cardObject.transform.localPosition = Vector3.zero;  // 카드의 위치를 중앙에 배치

            // 카드 UI 설정
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            if (cardUI != null)
            {
                cardUI.SetCardUI(selectedCard);  // 랜덤으로 뽑힌 카드 정보를 설정
            }
        }
    }
}

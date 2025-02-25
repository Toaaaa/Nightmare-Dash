using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합

    public Transform cardSpawnPoint;  // 카드가 생성될 위치
    public GameObject card;  // 카드 프리팹

    void Start()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
    }

    // 가중치 랜덤의 설명은 영상을 참고.
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
            GameObject cardObject = Instantiate(card, cardSpawnPoint);
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

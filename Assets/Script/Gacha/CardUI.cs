using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerDownHandler
{
    public Image cardImage;    // 카드 이미지
    public Text cardName;      // 카드 이름
    public Text cardType;      // 카드 유형
    public Text cardEffect;    // 카드 효과
    private Animator animator; // 카드 애니메이터

    private bool isFlipped = false; // 카드가 뒤집혔는지 확인

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 카드 UI 설정 메서드
    public void SetCardUI(Card card)
    {
        cardName.text = card.cardName;
        cardType.text = "Rarity: " + card.cardType;
        cardEffect.text = "Effect: " + card.cardEffect;

        if (cardImage != null)
        {
            cardImage.sprite = card.cardImage;
        }

        animator.SetTrigger("ShowCard");
    }

    // 카드 클릭 처리
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isFlipped)
        {
            animator.SetTrigger("Flip");
            isFlipped = true;
        }
        else
        {
            Destroy(gameObject); // 카드 제거
        }
    }
}

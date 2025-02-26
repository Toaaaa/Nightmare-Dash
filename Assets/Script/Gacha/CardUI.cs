using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image cardImage;    // 카드 이미지
    public Text cardName;      // 카드 이름
    public Text cardType;      // 카드 유형
    public Text cardEffect;    // 카드 효과
    private Animator animator; // 카드 애니메이터

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("🚨 Animator가 할당되지 않았습니다! CardUI 오브젝트에 Animator 컴포넌트가 있는지 확인하세요.", this);
        }
    }

    // ✅ 카드 UI 설정 메서드 (유물 정보 추가)
    public void SetCardUI(Card card)
    {
        if (card == null)
        {
            Debug.LogError("🚨 SetCardUI()에서 card가 null입니다! 카드가 생성되지 않았을 수 있습니다.", this);
            return;
        }

        gameObject.SetActive(true); // ✅ 카드 UI 활성화

        // ✅ 카드 기본 정보 설정
        cardName.text = !string.IsNullOrEmpty(card.cardName) ? card.cardName : "Unknown";
        cardType.text = !string.IsNullOrEmpty(card.cardType) ? "Rarity: " + card.cardType : "Rarity: Unknown";
        cardEffect.text = !string.IsNullOrEmpty(card.cardEffect) ? "Effect: " + card.cardEffect : "Effect: None";

        // ✅ 카드에 유물이 있을 경우 UI 업데이트
        if (card.artifact != null)
        {
            cardEffect.text += $"\n🛡️ Artifact: {card.artifact.Name ?? "Unknown"}";
            cardEffect.text += $"\n🔹 Rarity: {card.artifact.Rarity}";

            // ✅ 유물 효과 데이터가 없을 경우 기본값 설정
            if (card.artifact.Effect == null)
            {
                Debug.LogWarning($"⚠️ 카드 '{card.cardName}'의 유물 효과가 없습니다. 기본값(0)으로 설정합니다.", this);
                card.artifact.Effect = new Effect { Hp = 0, Currency = 0, Invincibility = 0 };
            }

            cardEffect.text += $"\n❤️ HP: {card.artifact.Effect.Hp}, 💰 Currency: {card.artifact.Effect.Currency}, 🛡️ Invincibility: {card.artifact.Effect.Invincibility}";

            // ✅ 유물 이미지가 있을 경우 카드 이미지로 설정
            if (cardImage != null)
            {
                cardImage.sprite = card.artifact.ArtifactImage ?? card.cardImage;
            }
        }
        else
        {
            // ✅ 카드 기본 이미지 설정
            if (cardImage != null)
            {
                cardImage.sprite = card.cardImage;
            }
        }

        // ✅ 애니메이션 실행 (animator가 null이면 실행 안 함)
        if (animator != null)
        {
            animator.SetTrigger("Flip");  // ✅ 카드가 보이면서 자동으로 뒤집히도록 설정
        }
        else
        {
            Debug.LogError("🚨 Flip() 실행 중 Animator가 null입니다!", this);
        }

        // ✅ 디버깅 로그
        Debug.Log($"✅ 카드 UI 설정 완료: {cardName.text}");
    }

    // ✅ 카드 뒤집기 애니메이션 실행
    public void Flip()
    {
        if (animator != null)
        {
            animator.SetTrigger("Flip");
        }
        else
        {
            Debug.LogError("🚨 Flip() 실행 중 Animator가 null입니다!", this);
        }
    }
}

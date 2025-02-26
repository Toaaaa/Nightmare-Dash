using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    public Image cardImage;    // 카드 이미지
    public Text cardName;      // 카드 이름
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

    // ✅ 카드 UI 설정 메서드 (유물 효과를 한글로 변환)
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

        // ✅ 카드에 유물이 있을 경우 UI 업데이트
        if (card.artifact != null)
        {
            cardEffect.text = card.artifact.GetEffectDescription(); // ✅ 효과 한글 변환

            // ✅ 유물 이미지 설정 (유물이 없으면 기본 카드 이미지 사용)
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

            // ✅ 효과 없음 표시
            cardEffect.text = "효과 없음";
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
        Debug.Log($"✅ 카드 UI 설정 완료: {cardName.text} | 효과: {cardEffect.text}");
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

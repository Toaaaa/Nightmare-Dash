using UnityEngine;
using UnityEngine.UI;

public class PetCardUI : MonoBehaviour
{
    public Image petImage;    // 펫 이미지
    public Text petName;      // 펫 이름
    public Text petDescription; // 펫 설명
    private Animator animator; // 애니메이션

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("🚨 Animator가 할당되지 않았습니다! PetCardUI 오브젝트에 Animator 컴포넌트가 있는지 확인하세요.");
        }
    }

    // ✅ 펫 UI 설정 메서드
    public void SetPetUI(PetData pet)
    {
        if (pet == null)
        {
            Debug.LogError("🚨 SetPetUI()에서 pet가 null입니다!");
            return;
        }

        gameObject.SetActive(true); // 펫 카드 UI 활성화

        petName.text = pet.PetName;
        petDescription.text = pet.PetDescription;

        // 펫 이미지 설정
        if (petImage != null)
        {
            petImage.sprite = pet.PetImage ?? petImage.sprite; // 이미지가 null이면 기본 이미지를 사용
        }

        // 애니메이션 실행 (flip)
        if (animator != null)
        {
            animator.SetTrigger("Flip");
        }
    }

    // 카드 뒤집기 애니메이션 실행
    public void Flip()
    {
        if (animator != null)
        {
            animator.SetTrigger("Flip");
        }
    }
}

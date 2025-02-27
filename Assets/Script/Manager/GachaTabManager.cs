using UnityEngine;
using UnityEngine.UI;

public class GachaTabManager : MonoBehaviour
{
    public GameObject artifactGachaCanvas;  // 유물 가챠 캔버스
    public GameObject petGachaCanvas;       // 펫 가챠 캔버스
    public Button artifactBtn;              // 유물 가챠 버튼
    public Button petBtn;                   // 펫 가챠 버튼

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        artifactBtn.onClick.AddListener(ShowArtifactGacha);
        petBtn.onClick.AddListener(ShowPetGacha);

        // 기본값: 유물 가챠 화면 활성화, 펫 가챠 화면 비활성화
        ShowArtifactGacha();
    }

    // 유물 가챠 활성화, 펫 가챠 비활성화
    public void ShowArtifactGacha()
    {
        artifactGachaCanvas.SetActive(true);
        petGachaCanvas.SetActive(false);

        // ✅ ArtifactBtn을 맨 위로 올리기
        artifactBtn.transform.SetAsLastSibling();

        Debug.Log("🔹 유물 가챠 화면 활성화 (ArtifactBtn이 위로 이동)");
    }

    // 펫 가챠 활성화, 유물 가챠 비활성화
    public void ShowPetGacha()
    {
        artifactGachaCanvas.SetActive(false);
        petGachaCanvas.SetActive(true);

        // ✅ PetBtn을 맨 위로 올리기
        petBtn.transform.SetAsLastSibling();

        Debug.Log("🔹 펫 가챠 화면 활성화 (PetBtn이 위로 이동)");
    }
}

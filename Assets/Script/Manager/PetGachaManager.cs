using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PetGachaManager : MonoBehaviour
{
    public Button DrawOneBtn, DrawFiveBtn, ExitBtn;
    public Image GachaFadeBlack;
    public float fadeDuration = 1.0f;
    private float maxAlpha = 0.5f;

    [SerializeField]
    private PetCardUI[] petCards; // ✅ 여러 장의 펫 카드 UI 배열

    private Diamond diamond; // 유료 재화

    private readonly int gachaPrice = 100; // 가챠 1회 가격

    private void Start()
    {
        if (petCards == null || petCards.Length == 0)
        {
            Debug.LogError("🚨 'petCards' 배열이 비어 있습니다! Inspector에서 PetCardUI 오브젝트들을 연결하세요.");
            return;
        }

        foreach (var petCard in petCards)
        {
            if (petCard == null)
            {
                Debug.LogError("🚨 'petCards' 배열 내에 PetCardUI가 없는 요소가 있습니다! Inspector에서 확인하세요.");
                return;
            }
        }

        diamond = DataManager.Instance.Diamond;

        DrawOneBtn.interactable = diamond.IsCanUse(gachaPrice);
        DrawFiveBtn.interactable = diamond.IsCanUse(gachaPrice * 5);

        DrawOneBtn.onClick.AddListener(() => DrawOneBtnClick(1));
        DrawFiveBtn.onClick.AddListener(() => DrawOneBtnClick(5));
        ExitBtn.onClick.AddListener(HideCardAndFadeBlack);
        ExitBtn.gameObject.SetActive(false);
    }

    public void DrawOneBtnClick(int num)
    {
        diamond.Use(gachaPrice * num);
        StartCoroutine(FadeInEffect(num));
    }

    private IEnumerator FadeInEffect(int num)
    {
        GachaFadeBlack.gameObject.SetActive(true);
        GachaFadeBlack.color = new Color(0, 0, 0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, elapsedTime / fadeDuration);
            GachaFadeBlack.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        StartCoroutine(ShowPetCardSlowly(num));
    }

    private IEnumerator ShowPetCardSlowly(int gachaCount)
    {
        for (int i = 0; i < gachaCount; i++)
        {
            if (i < petCards.Length)
            {
                SpawnRandomPet(petCards[i]);  // ✅ 여러 장의 펫 카드 UI 활용
            }
            else
            {
                Debug.LogWarning($"⚠️ {i + 1}번째 카드를 뽑으려고 했으나 'petCards' 배열의 크기를 초과했습니다.");
            }
            yield return new WaitForSeconds(0.1f);
        }

        ExitBtn.gameObject.SetActive(true);
    }

    // ✅ 기존 카드 오브젝트를 활용하여 펫 카드 UI 업데이트
    public void SpawnRandomPet(PetCardUI petCardUI)
    {
        if (petCardUI == null)
        {
            Debug.LogError("🚨 'PetCardUI' 오브젝트를 찾을 수 없습니다!");
            return;
        }

        petCardUI.gameObject.SetActive(true);

        PetSelect petSelect = FindObjectOfType<PetSelect>();
        if (petSelect == null)
        {
            Debug.LogError("🚨 'PetSelect' 오브젝트를 찾을 수 없습니다! 'Deck' 오브젝트에 PetSelect 스크립트가 있는지 확인하세요.");
            return;
        }

        PetData selectedPet = petSelect.GetRandomPet();
        if (selectedPet == null)
        {
            Debug.LogError("🚨 선택된 펫이 null입니다! PetSelect에서 올바르게 생성되는지 확인하세요.");
            return;
        }

        Debug.Log($"✅ 랜덤 펫 선택: {selectedPet.PetName}");

        petCardUI.SetPetUI(selectedPet);

        // ✅ 뽑은 펫을 플레이어 데이터에 추가
        if (selectedPet != null)
        {
            OnGachaResult(selectedPet);
        }
    }

    public void HideCardAndFadeBlack()
    {
        GachaFadeBlack.gameObject.SetActive(false);
        ResetCardState();
        DrawOneBtn.interactable = true;
        DrawFiveBtn.interactable = true;
        ExitBtn.gameObject.SetActive(false);
    }

    public void ResetForNewDraw()
    {
        HideCardAndFadeBlack();
        DrawOneBtn.interactable = false;
        DrawFiveBtn.interactable = false;
        StartCoroutine(FadeInEffect(1));
    }

    private void ResetCardState()
    {
        foreach (var petCard in petCards)
        {
            if (petCard != null)
            {
                petCard.gameObject.SetActive(false);
            }
        }
    }

    // ✅ 가챠에서 뽑힌 펫을 플레이어가 획득하도록 적용
    public void OnGachaResult(PetData pet)
    {
        if (pet == null)
        {
            Debug.LogError("🚨 뽑힌 펫이 없습니다!");
            return;
        }

        if (GameManager.instance == null || GameManager.instance.playerData == null)
        {
            Debug.LogError("🚨 GameManager 또는 PlayerData가 null입니다! GameManager가 정상적으로 로드되었는지 확인하세요.");
            return;
        }

        // ✅ 플레이어에게 펫 추가
        GameManager.instance.playerData.AddPet(pet);

        // ✅ DataManager가 초기화될 때까지 기다린 후 실행
        Debug.Log($"🎁 플레이어가 '{pet.PetName}' 펫을 획득했습니다!");
    }
}

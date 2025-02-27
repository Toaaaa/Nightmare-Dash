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
            return;
        }

        foreach (var petCard in petCards)
        {
            if (petCard == null)
            {
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
            yield return new WaitForSeconds(0.1f);
        }

        ExitBtn.gameObject.SetActive(true);
    }

    // ✅ 기존 카드 오브젝트를 활용하여 펫 카드 UI 업데이트
    public void SpawnRandomPet(PetCardUI petCardUI)
    {
        if (petCardUI == null)
        {
            return;
        }

        petCardUI.gameObject.SetActive(true);

        PetSelect petSelect = FindObjectOfType<PetSelect>();
        if (petSelect == null)
        {
            return;
        }

        PetData selectedPet = petSelect.GetRandomPet();
        if (selectedPet == null)
        {
            return;
        }

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
            return;
        }

        if (GameManager.instance == null || GameManager.instance.playerData == null)
        {
            return;
        }

        // ✅ 플레이어에게 펫 추가
        GameManager.instance.playerData.AddPet(pet);
    }
}

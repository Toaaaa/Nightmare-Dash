using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DOTween 사용

public class PlayerCustomUI : BaseUI
{
    [SerializeField] private Button customizeButton;

    [Header("Tabs")]
    [SerializeField] private Button achievementTab;
    [SerializeField] private Button petTab;
    [SerializeField] private Button relicTab;
    private Button currentTab; // 현재 선택된 탭 추적

    [Header("Close Button")]
    [SerializeField] private Button closeButton;

    [Header("Panels")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject petPanel;
    [SerializeField] private GameObject relicPanel;
    [SerializeField] private GameObject customizeUIPanel;

    [Header("Slots")]
    [SerializeField] private Transform slotContainer;
    [SerializeField] private Transform petSlotContainer;  // 펫 슬롯 리스트
    [SerializeField] private Transform relicSlotContainer; // 유물 슬롯 리스트
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject petslotPrefab;
    [SerializeField] private GameObject relicslotPrefab;


    [Header("Description")]
    [SerializeField] private TMP_Text descriptionText;

    public static PlayerCustomUI Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        GameManager.instance.LoadPlayerData();
    }
    private void Start()
    {
        // AchievementManager가 존재하는지 확인 후 이벤트 등록
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementUnlocked += UpdateAchievementUI;
            UpdateAchievementUI();
            
        }
        else
        {
            Debug.LogError("AchievementManager.Instance가 null입니다. 씬 로딩 순서를 확인하세요.");
        }

        // 버튼 이벤트 설정
        customizeButton.onClick.AddListener(OpenCustomizeUI);
        achievementTab.onClick.AddListener(() => ShowPanel(achievementPanel, achievementTab));
        petTab.onClick.AddListener(() => ShowPanel(petPanel, petTab));
        relicTab.onClick.AddListener(() => ShowPanel(relicPanel, relicTab));
        closeButton.onClick.AddListener(CloseUI);

        // 기본 패널 설정
        ShowPanel(achievementPanel, achievementTab);

        LoadArtifactSlots();
    }


    public void UpdateAchievementUI(string achievementName = null)
    {
        // 기존 슬롯 삭제
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        // 업적 목록 불러오기
        List<Achievement> achievements = AchievementManager.Instance.achievements;
        foreach (var achievement in achievements)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotContainer); // 프리팹 인스턴스
            TMP_Text slotText = newSlot.GetComponentInChildren<TMP_Text>();//텍스트메시프로만 가져옴
            if (slotText != null)
                slotText.text = achievement.Name;
                
            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null) 
                slotButton.onClick.AddListener(() => ShowDescription(achievement.Name)); slotButton.enabled = true;


            Image slotImage = newSlot.GetComponent<Image>();
            if (slotImage != null)
                slotImage.color = achievement.IsUnlocked ? Color.white : Color.gray; slotImage.enabled = true;

        }
    }
    private void OpenCustomizeUI()
    {
        customizeUIPanel.SetActive(true);
        customizeUIPanel.transform.localScale = Vector3.zero;
        customizeUIPanel.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack); // 등장 애니메이션
        ShowPanel(achievementPanel, achievementTab);
    }

    private void ShowPanel(GameObject activePanel, Button activeTab)
    {
        // 모든 패널 비활성화 후 선택된 패널 활성화
        achievementPanel.SetActive(false);
        petPanel.SetActive(false);
        relicPanel.SetActive(false);
        activePanel.SetActive(true);

        

        // 선택된 탭 스타일 변경
        if (currentTab != null)
            currentTab.GetComponent<Image>().color = Color.gray; // 기본 색상

        activeTab.GetComponent<Image>().color = Color.white; // 활성화된 색상
        currentTab = activeTab;


    }

    // 펫 슬롯 로드
    public void LoadPetSlots()
    {

        foreach (Transform child in petSlotContainer) Destroy(child.gameObject);
        List<PetData> pets = DataManager.Instance.PetManager.Pets;
        foreach (var pet in pets)
        {
            GameObject newSlot = Instantiate(petslotPrefab, petSlotContainer);
            TMP_Text slotText = newSlot.GetComponentInChildren<TMP_Text>();
            if (slotText != null)
                slotText.text = pet.PetName;
            slotText.color = pet.IsObtained ? Color.white : Color.gray; // 획득 여부에 따른 색상 변경
            Image slotImage = newSlot.GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.color = Color.white;
                slotImage.enabled = true;
                slotImage.color = pet.IsObtained ? new Color(1, 1, 1, 1f) : new Color(1, 1, 1, 0.5f);

            }
            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null)
                slotButton.onClick.AddListener(() => ShowDescription(pet.PetName)); slotButton.enabled = true;

            // 설명 표시 이벤트 추가
            newSlot.GetComponent<Button>().onClick.AddListener(() => ShowDescription(pet.PetName));
        }
    }

    // 유물 슬롯 로드 (플레이어 보유 유물만 표시)
    public void LoadArtifactSlots()
    {
        // GameManager.instance 또는 playerData가 null인지 확인
        if (GameManager.instance == null || GameManager.instance.playerData == null)
        {
            Debug.LogError("GameManager.instance 또는 GameManager.instance.playerData가 null입니다.");
            return;
        }

        // 기존 슬롯 삭제
        foreach (Transform child in relicSlotContainer) Destroy(child.gameObject);
        // 플레이어가 보유한 유물 리스트 가져오기
        List<ArtifactData> ownedArtifacts = GameManager.instance.playerData.OwnedArtifacts;
        Debug.Log($"OwnedArtifacts 개수: {ownedArtifacts.Count}");
        // 유물 슬롯 생성
        foreach (var artifact in ownedArtifacts)
        {
            GameObject newSlot = Instantiate(relicslotPrefab, relicSlotContainer);
            TMP_Text slotText = newSlot.GetComponentInChildren<TMP_Text>();
            if (slotText != null)
                slotText.text = artifact.Name;
            slotText.color = artifact.IsObtained ? Color.black : Color.gray; // 획득 여부에 따른 색상 변경
            Image slotImage = newSlot.GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.color = Color.white;
                slotImage.enabled = true;
                slotImage.color = artifact.IsObtained ? new Color(1, 1, 1, 1f) : new Color(1, 1, 1, 0.5f);
            }
            Sprite slotSprite = newSlot.GetComponent<Sprite>();
                slotImage.sprite = slotSprite;
            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null)
                slotButton.onClick.AddListener(() => ShowDescription(artifact.Name)); slotButton.enabled = true;

            // 설명 표시 이벤트 추가
            newSlot.GetComponent<Button>().onClick.AddListener(() => ShowDescription(artifact.Name));
        }
    }
    private void ShowDescription(string itemName)
    {
        descriptionText.text = $"{itemName} 설명을 여기에 표시";
        descriptionText.DOFade(0, 0);  // 투명하게 초기화
        descriptionText.DOFade(1, 0.3f); // 점점 나타나는 효과
    }

    private void CloseUI()
    {
        customizeUIPanel.transform.DOScale(0, 0.2f).SetEase(Ease.InBack)
            .OnComplete(() => customizeUIPanel.SetActive(false)); // 애니메이션 후 비활성화
        uiManager.SetUIState(UIState.Lobby);
    }
    private void OnDestroy()
    {
        // 이벤트 해제 (메모리 누수 방지)
        if (AchievementManager.Instance != null)
        {
            AchievementManager.Instance.OnAchievementUnlocked -= UpdateAchievementUI;
        }
    }
    protected override UIState GetUIState()
    {
        return UIState.PlayerCustom;
    }
}

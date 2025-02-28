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
    [Header("Pets")]
    [SerializeField] private GameObject currentPet;

    [Header("Description")]
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image descriptionImg;

    public static PlayerCustomUI Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        //GameManager.instance.LoadPlayerData();
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
                slotButton.onClick.AddListener(() => ShowDescription(achievement.Description,null)); slotButton.enabled = true;


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
        // GameManager.instance 또는 playerData가 null인지 확인
        if (GameManager.instance == null || GameManager.instance.playerData == null)
        {
            Debug.LogError("GameManager.instance 또는 GameManager.instance.playerData가 null입니다.");
            return;
        }

        // 기존 슬롯 삭제
        foreach (Transform child in petSlotContainer) Destroy(child.gameObject);

        // 플레이어가 보유한 유물 리스트 가져오기
        List<PetData> ownedPets = GameManager.instance.playerData.OwnedPets;
        // 유물 슬롯 생성
        foreach (var pets in ownedPets)
        {
            GameObject newSlot = Instantiate(petslotPrefab, petSlotContainer);
            TMP_Text slotText = newSlot.GetComponentInChildren<TMP_Text>();
            if (slotText != null)
                slotText.text = pets.PetName;
            slotText.color = pets.IsObtained ? Color.black : Color.gray; // 획득 여부에 따른 색상 변경
            Image slotImage = newSlot.transform.Find("Icon").GetComponent<Image>();//이름이 Icon인 오브젝트의 Image 컴포넌트
            Image slotbackgroundimg = newSlot.GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.sprite = pets.PetImage;

                slotbackgroundimg.color = Color.white;
                slotbackgroundimg.enabled = true;
                slotImage.enabled = true;
                slotbackgroundimg.color = pets.IsObtained ? new Color(1, 1, 1, 1f) : new Color(1, 1, 1, 0.5f);
            }

            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null)
                slotButton.onClick.AddListener(() => ShowDescription(pets.PetDescription, pets.PetImage,true)); slotButton.enabled = true;
            slotButton.onClick.AddListener(() => EquipPet(pets));

            // 설명 표시 이벤트 추가
            newSlot.GetComponent<Button>().onClick.AddListener(() => ShowDescription(pets.PetDescription,pets.PetImage));
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
        // 유물 슬롯 생성
        foreach (var artifact in ownedArtifacts)
        {
            GameObject newSlot = Instantiate(relicslotPrefab, relicSlotContainer);
            TMP_Text slotText = newSlot.GetComponentInChildren<TMP_Text>();
            if (slotText != null)
                slotText.text = artifact.Name;
            slotText.color = artifact.IsObtained ? Color.black : Color.gray; // 획득 여부에 따른 색상 변경
            Image slotImage = newSlot.transform.Find("Icon").GetComponent<Image>();//이름이 Icon인 오브젝트의 Image 컴포넌트
            Image slotbackgroundimg = newSlot.GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.sprite = artifact.ArtifactImage;
                
                slotbackgroundimg.color = Color.white;
                slotbackgroundimg.enabled = true;
                slotImage.enabled = true;
                slotbackgroundimg.color = artifact.IsObtained ? new Color(1, 1, 1, 1f) : new Color(1, 1, 1, 0.5f);
            }
            
            Button slotButton = newSlot.GetComponent<Button>();
            if (slotButton != null)
                slotButton.onClick.AddListener(() => ShowDescription(artifact.Name,artifact.ArtifactImage)); slotButton.enabled = true;

            // 설명 표시 이벤트 추가
            newSlot.GetComponent<Button>().onClick.AddListener(() => ShowDescription(artifact.Name,artifact.ArtifactImage));
        }
    }
    private void ShowDescription(string itemName, Sprite itemSprite, bool isPet = false)
    {
        if (string.IsNullOrEmpty(itemName))
            descriptionText.text = "설명을 여기에 표시";
        else descriptionText.text = itemName;

        if (descriptionImg != null)
        {
            descriptionImg.sprite = itemSprite;
            descriptionImg.color = new Color(1, 1, 1, 1); // 투명도를 초기화 (이미지 표시)
        }
        if (isPet)
        {
            SpawnPet(itemSprite);
        }

    }
    public void SpawnPet(Sprite petSprite)
    {
        if (currentPet != null)
        {
            Destroy(currentPet); // 기존 펫 삭제
        }

        // 플레이어 오브젝트 찾기
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다!");
            return;
        }

        // 새 펫 오브젝트 생성
        currentPet = new GameObject("Pet");
        currentPet.transform.SetParent(player.transform); // 플레이어의 하위 오브젝트로 설정
        currentPet.transform.localPosition = new Vector3(-1, -2, 0); // 플레이어 기준 오른쪽에 배치

        // 스프라이트 렌더러 추가 및 설정
        SpriteRenderer petRenderer = currentPet.AddComponent<SpriteRenderer>();
        petRenderer.sprite = petSprite;
        petRenderer.sortingOrder = 5; // 플레이어보다 앞에 렌더링되도록 설정
        currentPet.transform.localScale = new Vector3(4f, 4f, 1f); // 원하는 크기로 조정
    }

    private void EquipPet(PetData pet)
    {
        // 플레이어 데이터에 펫 정보 저장
        GameManager.instance.playerData.EquipPet(pet);

        // 데이터 저장 (파일, PlayerPrefs 등)
        GameManager.instance.SavePlayerData();

        //펫을 플레이어 하위 오브젝트로 소환
        SpawnPet(pet.PetImage);

        (SceneBase.Current as MainLobbySceneController).EquipPetImage = pet;
    }


    public void LoadEquippedPet()
    {
        string savedPetID = GameManager.instance.playerData.equippedPetID;

        if (!string.IsNullOrEmpty(savedPetID))
        {
            // 저장된 펫 ID로 PetData 검색
            PetData equippedPet = GameManager.instance.playerData.OwnedPets.Find(p => p.Id.ToString() == savedPetID);

            if (equippedPet != null)
            {
                SpawnPet(equippedPet.PetImage); // 펫 생성
            }
            else
            {
                Debug.LogWarning("저장된 펫을 찾을 수 없습니다!");
            }
        }
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

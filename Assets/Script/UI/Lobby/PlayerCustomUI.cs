using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomUI : BaseUI
{

    [SerializeField] private Button customizeButton;
    [Header("Tabs")]
    [SerializeField] private Button achievementTab;
    [SerializeField] private Button petTab;
    [SerializeField] private Button relicTab;
    [Header("CloseButton")]
    [SerializeField] private Button closeButton;
    [Header("Panel")]
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject petPanel;
    [SerializeField] private GameObject relicPanel;
    [SerializeField] private GameObject customizeUIPanel; // 전체 패널
    [Header("이건 뭐라하지")]
    [SerializeField] private Transform slotContainer; // 슬롯 리스트
    [SerializeField] private GameObject slotPrefab; // 슬롯 프리팹
    [SerializeField] private TMP_Text descriptionText; // 설명 표시

    private void Start()
    {
        // UI 버튼 이벤트 연결
        customizeButton.onClick.AddListener(() => OpenCustomizeUI());
        achievementTab.onClick.AddListener(() => ShowPanel(achievementPanel));
        petTab.onClick.AddListener(() => ShowPanel(petPanel));
        relicTab.onClick.AddListener(() => ShowPanel(relicPanel));
        closeButton.onClick.AddListener(CloseUI);

        
        ShowPanel(achievementPanel); // 기본적으로 업적 탭 활성화
    }

    private void OpenCustomizeUI()
    {
        customizeUIPanel.SetActive(true); // 전체 UI 활성화
        ShowPanel(achievementPanel); // 기본으로 업적 탭 활성화
    }

    private void ShowPanel(GameObject activePanel)
    {
        // 모든 패널 비활성화 후, 선택된 패널만 활성화
        achievementPanel.SetActive(false);
        petPanel.SetActive(false);
        relicPanel.SetActive(false);

        activePanel.SetActive(true);
    }

    public void LoadItems(List<string> items)// 아이템 리스트 불러오기
    {
        foreach (Transform child in slotContainer) Destroy(child.gameObject);

        foreach (var item in items)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotContainer);
            newSlot.GetComponentInChildren<TMP_Text>().text = item;
            newSlot.GetComponent<Button>().onClick.AddListener(() => ShowDescription(item));
        }
    }

    private void ShowDescription(string itemName)
    {
        descriptionText.text = $"{itemName} 설명을 여기에 표시";
    }

    private void CloseUI()
    {
        customizeUIPanel.SetActive(false); // UI 패널 닫기
        uiManager.SetUIState(UIState.Lobby); // 로비 UI로 복귀
    }




    protected override UIState GetUIState()
    {
        return UIState.PlayerCustom;
    }
}
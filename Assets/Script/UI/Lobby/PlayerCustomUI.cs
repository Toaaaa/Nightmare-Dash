using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomUI : BaseUI
{
    [SerializeField] private Button customizeButton;
    [SerializeField] private Button achievementTab;
    [SerializeField] private Button petTab;
    [SerializeField] private Button relicTab;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject petPanel;
    [SerializeField] private GameObject relicPanel;
    [SerializeField] private GameObject customizeUIPanel; // 전체 패널

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
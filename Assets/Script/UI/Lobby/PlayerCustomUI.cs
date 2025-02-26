using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomUI : BaseUI
{
    [SerializeField] private Button custommizeButton;
    [SerializeField] private Button achievementTab;
    [SerializeField] private Button petTab;
    [SerializeField] private Button relicTab;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject petPanel;
    [SerializeField] private GameObject relicPanel;

    [SerializeField] private GameObject customizeUIPanel;

    private void Start()
    {
        custommizeButton.onClick.AddListener(() => ShowPanel(customizeUIPanel));
        achievementTab.onClick.AddListener(() => ShowPanel(achievementPanel));
        petTab.onClick.AddListener(() => ShowPanel(petPanel));
        relicTab.onClick.AddListener(() => ShowPanel(relicPanel));
        closeButton.onClick.AddListener(CloseUI);

        ShowPanel(achievementPanel); // 기본적으로 업적 탭 활성화
    }

    private void ShowPanel(GameObject activePanel)
    {
        customizeUIPanel.SetActive(activePanel == customizeUIPanel);
        achievementPanel.SetActive(activePanel == achievementPanel);
        petPanel.SetActive(activePanel == petPanel);
        relicPanel.SetActive(activePanel == relicPanel);
        // 선택한 패널 활성화
        activePanel.SetActive(true);
    }

    private void CloseUI()
    {
        customizeUIPanel.gameObject.SetActive(false);
        uiManager.SetUIState(UIState.Lobby); // 로비 UI 다시 활성화
    }

    protected override UIState GetUIState()
    {
        return UIState.PlayerCustom;
    }
}
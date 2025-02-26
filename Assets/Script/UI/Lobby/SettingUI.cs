using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] private GameObject settingPanel; // 설정 패널
    [SerializeField] private Button settingButton;    // 설정 버튼
    [SerializeField] private GameObject lobbyUI; // 로비 UI

    private void OnEnable()
    {
        settingButton.onClick.AddListener(OpenSettingPanel);
    }

    private void OnDisable()
    {
        settingButton.onClick.RemoveListener(OpenSettingPanel);
    }

    private void Start()
    {
        settingPanel.SetActive(false);
    }

    private void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        lobbyUI.SetActive(false);

        // UIManager를 이용해 상태 전환
        if (uiManager != null)
        {
            uiManager.SetUIState(UIState.Settings);
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.Settings; // UIState.Lobby → UIState.Settings로 변경
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] private GameObject settingPanel; // 설정 패널
    [SerializeField] private Button settingButton;    //설정 버튼
    [SerializeField] private GameObject lobbyUI; // 로비 UI


    private void Start()
    {
        settingButton.onClick.AddListener(OpenSettingPanel);
        settingPanel.SetActive(false);
    }

    private void OpenSettingPanel()
    {
        // 설정 UI 활성화, 로비 UI 비활성화
        settingPanel.SetActive(true);
        lobbyUI.SetActive(false);
    }
    protected override UIState GetUIState()
    {
        return UIState.Lobby;

    }

    private void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 
    #else
        Application.Quit();  // 종료
    #endif
    }

}

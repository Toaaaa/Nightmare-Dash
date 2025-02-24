using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField] private GameObject settingPanel; // ���� �г� ����
    [SerializeField] private Button settingButton;    // ���� ��ư
    [SerializeField] private GameObject lobbyUI; // �κ� UI


    private void Start()
    {
        settingButton.onClick.AddListener(OpenSettingPanel);
        settingPanel.SetActive(false);
    }

    private void OpenSettingPanel()
    {
        // ���� UI Ȱ��ȭ, �κ� UI ��Ȱ��ȭ
        settingPanel.SetActive(true);
        lobbyUI.SetActive(false);
    }
    protected override UIState GetUIState()
    {
        return UIState.Lobby;

    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);  // ���� â �ݱ�
    }

    private void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ����
    #else
        Application.Quit();  // ����� ���� ����
    #endif
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyUI : BaseUI
{

    [SerializeField] private Button lobbyButton; // ✅ 로비 이동 버튼 추가

    private void Start()
    {
        if (lobbyButton != null)
        {
            lobbyButton.onClick.AddListener(OnLobbyClick);
        }
        else
        {
            Debug.LogError("🚨 Lobby 버튼이 할당되지 않았습니다! Unity Inspector에서 확인하세요.");
        }
    }

    private void OnLobbyClick()
    {
        Debug.Log("🏠 로비로 이동!");
        SceneManager.LoadScene("MainLobby"); // ✅ MainLobby 씬으로 이동
    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}

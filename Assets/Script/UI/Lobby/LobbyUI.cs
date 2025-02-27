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
    }

    private void OnLobbyClick()
    {
        SceneManager.LoadScene("MainLobby"); // ✅ MainLobby 씬으로 이동
    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}

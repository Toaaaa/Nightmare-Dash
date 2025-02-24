using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : BaseUI
{
    [SerializeField] private Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClick);
    }

    private void OnStartClick()
    {
        uiManager.SetUIState(UIState.Game);

    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}
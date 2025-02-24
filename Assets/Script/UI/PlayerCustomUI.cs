using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomUI : BaseUI
{
    [SerializeField] private Button customButton;

    private void Start()
    {
        customButton.onClick.AddListener(OnCustomClick);
    }

    private void OnCustomClick()
    {
        uiManager.SetUIState(UIState.PlayerCustom);
    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}


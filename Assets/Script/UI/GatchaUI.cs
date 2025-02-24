using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatchaUI : BaseUI
{
    [SerializeField] private Button gatchaButton;

    private void Start()
    {
        gatchaButton.onClick.AddListener(OnGatchaClick);
    }

    private void OnGatchaClick()
    {
        uiManager.SetUIState(UIState.Gatcha);
    }

    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}

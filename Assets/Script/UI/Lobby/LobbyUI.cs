using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    protected override UIState GetUIState()
    {
        return UIState.Lobby;
    }
}

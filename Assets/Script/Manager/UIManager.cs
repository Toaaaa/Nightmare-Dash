using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIState
{
    Lobby,       // 로비 화면
    Game,        // 게임 화면
    Gatcha,      // 가챠 화면
    PlayerCustom, // 커스텀 화면
    Settings      // 설정 화면
}
public class UIManager : MonoBehaviour
{
    [SerializeField] private List<BaseUI> uiElements;
    private UIState currentState;

    private void Start()
    {
        foreach (var ui in uiElements)
        {
            ui.Init(this);
        }
        SetUIState(UIState.Lobby); // 기본 상태를 로비로 설정
    }

    public void SetUIState(UIState newState)
    {
        currentState = newState;
        foreach (var ui in uiElements)
        {
            ui.SetActive(newState);
        }
    }

    public void SetActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }


    public UIState GetCurrentState()
    {
        return currentState;
    }
}



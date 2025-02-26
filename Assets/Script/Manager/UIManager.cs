using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Lobby,       // 로비 화면
    PlayerCustom, // 커스텀 화면
    Settings      // 설정 화면
}

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<BaseUI> uiElements;
    private Dictionary<UIState, BaseUI> uiDictionary = new Dictionary<UIState, BaseUI>();
    private UIState currentState;

    private void Start()
    {
        if (uiElements == null || uiElements.Count == 0)
        {
            Debug.LogError("[UIManager] UI 요소 리스트가 비어 있습니다!");
            return;
        }

        // Dictionary로 변환하여 빠른 접근 가능
        foreach (var ui in uiElements)
        {
            ui.Init(this);
            UIState state = ui.GetState();
            if (!uiDictionary.ContainsKey(state))
            {
                uiDictionary.Add(state, ui);
            }
            else
            {
                Debug.LogWarning($"[UIManager] 중복된 UIState: {state}가 감지되었습니다.");
            }
        }

        SetUIState(UIState.Lobby); // 기본 상태를 로비로 설정
    }

    public void SetUIState(UIState newState)
    {
        if (currentState == newState) return; // 동일한 상태 변경 방지

        currentState = newState;

    }

    public UIState GetCurrentState()
    {
        return currentState;
    }
}

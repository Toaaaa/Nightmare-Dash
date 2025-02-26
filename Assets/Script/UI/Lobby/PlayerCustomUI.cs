using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomUI : BaseUI
{
    [SerializeField] private GameObject customUI; // 커스터마이징 패널
    [SerializeField] private Button customButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        customButton.onClick.AddListener(OnCustomClick);
        closeButton.onClick.AddListener(CloseCustomPanel);
        
        // 초기 UI 설정
        customUI.SetActive(false);  // 커스터마이징 UI 비활성화
    }

    private void CloseCustomPanel()
    {
        customUI.SetActive(false);  // 커스터마이징 UI 숨기기
    }

    private void OnCustomClick()
    {
        if (uiManager != null)
        {
            uiManager.SetUIState(UIState.PlayerCustom);
            customUI.SetActive(true);   // 커스터마이징 UI 활성화
        }
        else
        {
            Debug.LogError("UIManager가 설정되지 않았습니다.");
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.PlayerCustom;
    }
}
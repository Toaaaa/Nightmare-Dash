using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    protected UIManager uiManager;

    // UI 초기화 (UIManager 할당)
    public virtual void Init(UIManager uiManager)
    {
        if (uiManager == null)
        {
            throw new System.ArgumentNullException(nameof(uiManager), "[BaseUI] UIManager가 설정되지 않았습니다!");
        }
        this.uiManager = uiManager;
    }

    // 현재 UI의 상태 반환 (추상 메서드)
    protected abstract UIState GetUIState();

    // UIManager에서 호출할 수 있는 public 메서드 추가
    public UIState GetState()
    {
        return GetUIState();
    }

    // UI 상태에 따라 활성화/비활성화
    public void SetActive(UIState state)
    {
        bool shouldBeActive = (GetUIState() == state);
        if (gameObject.activeSelf == shouldBeActive) return; // 불필요한 호출 방지

        if (shouldBeActive)
        {
            OnShow();
        }
        else
        {
            OnHide();
        }
        gameObject.SetActive(shouldBeActive);
    }

    // UI가 활성화될 때 실행할 메서드 (필요하면 오버라이드 가능)
    protected virtual void OnShow() { }

    // UI가 비활성화될 때 실행할 메서드 (필요하면 오버라이드 가능)
    protected virtual void OnHide() { }
}



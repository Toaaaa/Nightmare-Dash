using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private Button closeButton = null; // 닫기 버튼

    [SerializeField]
    private Button blockerButton = null; // 팝업창 뒷부분

    [SerializeField]
    private Animator popupAnimator;

    public Action OnBeginOpenEvent = delegate { };  // 팝업창이 열릴때 호출되는 이벤트
    public Action OnEndOpenEvent = delegate { };    // 팝업창이 다 열렸을때 호출되는 이벤트
    public Action OnBeginCloseEvent = delegate { }; // 팝업창을 닫을때 호출되는 이벤트
    public Action OnEndCloseEvent = delegate { };   // 팝업창을 다 닫았을때 호출돠는 이벤트

    private void Start()
    {
        if (closeButton == null && blockerButton == null)
        {
            return;
        }

        // 닫기 버튼이 설정되어있으면 닫기 버튼으로 팝업창 닫기
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(Close);
        }
        // 아니라면 뒷 부분 클릭으로 닫기
        else
        {
            blockerButton.onClick.AddListener(Close);
        }
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        popupAnimator.SetTrigger("Close");
    }

    public void OnTriggerBeginOpenEvent()
    {
        OnBeginOpenEvent();
    }

    public void OnTriggerEndOpenEvent()
    {
        OnEndOpenEvent();
    }

    public void OnTriggerBeginCloseEvent()
    {
        OnBeginCloseEvent();
    }

    public void OnTriggerEndCloseEvent()
    {
        this.gameObject.SetActive(false);
        OnEndCloseEvent();
    }

}

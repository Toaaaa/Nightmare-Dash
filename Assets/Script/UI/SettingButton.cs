using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel; // 설정 UI 패널
    [SerializeField] private Button settingButton;    // 설정 버튼

    private void Start()
    {
        // 버튼 클릭 이벤트 연결
        settingButton.onClick.AddListener(ToggleSettingPanel);

        // 처음에는 설정 창 비활성화
        settingPanel.SetActive(false);
    }

    private void ToggleSettingPanel()
    {
        // 설정 UI 패널을 토글 (켜고 끄기)
        settingPanel.SetActive(true);
    }
}

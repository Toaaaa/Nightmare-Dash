using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel; // ���� UI �г�
    [SerializeField] private Button settingButton;    // ���� ��ư

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        settingButton.onClick.AddListener(ToggleSettingPanel);

        // ó������ ���� â ��Ȱ��ȭ
        settingPanel.SetActive(false);
    }

    private void ToggleSettingPanel()
    {
        // ���� UI �г��� ��� (�Ѱ� ����)
        settingPanel.SetActive(true);
    }
}

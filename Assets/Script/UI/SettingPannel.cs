using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPannel : MonoBehaviour
{
    //[SerializeField] private Toggle jumpscareToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject lobbyUI;

    private void Start()
    {
        // ���� ���� �ҷ�����
        //jumpscareToggle.isOn = PlayerPrefs.GetInt("JumpscareEnabled", 1) == 1;
        volumeSlider.value = GameSettingsManager.Instance.Volume;
        muteToggle.isOn = GameSettingsManager.Instance.IsMuted;

        // �̺�Ʈ ����
        //jumpscareToggle.onValueChanged.AddListener(SetJumpscare);
        volumeSlider.onValueChanged.AddListener(value => GameSettingsManager.Instance.Volume = value);
        muteToggle.onValueChanged.AddListener(value => GameSettingsManager.Instance.IsMuted = value);
        closeButton.onClick.AddListener(CloseSettings);
        quitButton.onClick.AddListener(QuitGame);

        gameObject.SetActive(false);

    }


    private void CloseSettings()
    {
        gameObject.SetActive(false);  // ���� â �ݱ�
        lobbyUI.SetActive(true);//�κ� �ٽ� Ȱ��ȭ
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // �����Ϳ��� ���� ����
#else
        Application.Quit();  // ����� ���� ����
#endif
    }
}

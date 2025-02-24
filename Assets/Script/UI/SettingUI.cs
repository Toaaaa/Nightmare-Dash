using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    //[SerializeField] private Toggle jumpscareToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        // ���� ���� �ҷ�����
        //jumpscareToggle.isOn = PlayerPrefs.GetInt("JumpscareEnabled", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;

        // �̺�Ʈ ����
        //jumpscareToggle.onValueChanged.AddListener(SetJumpscare);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        muteToggle.onValueChanged.AddListener(SetMute);
        closeButton.onClick.AddListener(CloseSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = muteToggle.isOn ? 0 : volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    private void SetMute(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0 : volumeSlider.value;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);  // ���� â �ݱ�
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

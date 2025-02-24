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
        // 기존 설정 불러오기
        //jumpscareToggle.isOn = PlayerPrefs.GetInt("JumpscareEnabled", 1) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        muteToggle.isOn = PlayerPrefs.GetInt("Muted", 0) == 1;

        // 이벤트 연결
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
        gameObject.SetActive(false);  // 설정 창 닫기
    }

    private void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // 에디터에서 실행 중지
    #else
        Application.Quit();  // 빌드된 게임 종료
    #endif
    }
}

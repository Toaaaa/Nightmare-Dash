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

    private void OnEnable()
    {
        // GameSettingsManager 인스턴스가 존재하는지 확인
        if (GameSettingsManager.Instance == null)
        {
            Debug.LogError("[SettingPannel] GameSettingsManager 인스턴스가 없습니다!");
            return;
        }

        // 기존 설정 불러오기
        volumeSlider.value = GameSettingsManager.Instance.Volume;
        muteToggle.isOn = GameSettingsManager.Instance.IsMuted;

        // 이벤트 연결
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteChanged);
        closeButton.onClick.AddListener(CloseSettings);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        // 이벤트 리스너 제거 (중복 등록 방지)
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        muteToggle.onValueChanged.RemoveListener(OnMuteChanged);
        closeButton.onClick.RemoveListener(CloseSettings);
        quitButton.onClick.RemoveListener(QuitGame);
    }

    private void OnVolumeChanged(float value)
    {
        if (GameSettingsManager.Instance != null)
        {
            GameSettingsManager.Instance.Volume = value;
        }
    }

    private void OnMuteChanged(bool isMuted)
    {
        if (GameSettingsManager.Instance != null)
        {
            GameSettingsManager.Instance.IsMuted = isMuted;
        }
    }

    private void CloseSettings()
    {
        gameObject.SetActive(false);  // 설정 창 닫기

        if (lobbyUI != null)
        {
            lobbyUI.SetActive(true); // 로비 다시 활성화
        }
        else
        {
            Debug.LogWarning("[SettingPannel] lobbyUI가 설정되지 않았습니다.");
        }
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

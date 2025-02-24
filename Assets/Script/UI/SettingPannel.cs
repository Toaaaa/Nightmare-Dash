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
        // 기존 설정 불러오기
        //jumpscareToggle.isOn = PlayerPrefs.GetInt("JumpscareEnabled", 1) == 1;
        volumeSlider.value = GameSettingsManager.Instance.Volume;
        muteToggle.isOn = GameSettingsManager.Instance.IsMuted;

        // 이벤트 연결
        //jumpscareToggle.onValueChanged.AddListener(SetJumpscare);
        volumeSlider.onValueChanged.AddListener(value => GameSettingsManager.Instance.Volume = value);
        muteToggle.onValueChanged.AddListener(value => GameSettingsManager.Instance.IsMuted = value);
        closeButton.onClick.AddListener(CloseSettings);
        quitButton.onClick.AddListener(QuitGame);

        gameObject.SetActive(false);

    }


    private void CloseSettings()
    {
        gameObject.SetActive(false);  // 설정 창 닫기
        lobbyUI.SetActive(true);//로비 다시 활성화
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

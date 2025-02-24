using TMPro;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [Header("인게임")]
    [SerializeField]
    public HpBar hpBar;
    [SerializeField]
    public TextMeshProUGUI scoreText;

    private GameSceneController sceneController;

    [Header("게임결과")]
    [SerializeField]
    private PopupInGameResult resultPopup;
    private PopupInGameResult instanceResultPopup;

    public void Initialize()
    {
        sceneController = SceneBase.Current as GameSceneController;
        
        //이벤트 설정
        sceneController.OnChangedScoreAmountEvnet += UpdateScoreText;
        sceneController.OnFinishGameEvent += ShowResultUI;
        sceneController.GameHp.OnChangedHpAmountEvent += hpBar.UpdateHpBar;

        hpBar.SetHpBar(sceneController.GameHp.MaxHp);
        UpdateScoreText(sceneController.Score);
    }

    private void OnDestroy()
    {
        sceneController.OnChangedScoreAmountEvnet -= UpdateScoreText;
        sceneController.OnFinishGameEvent -= ShowResultUI;
        sceneController.GameHp.OnChangedHpAmountEvent -= hpBar.UpdateHpBar;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = $"Score : {score}";
    }

    //결과창 표시
    public void ShowResultUI()
    {
        // 게임 결과창이 생성되어있지않다면
        if (instanceResultPopup == null)
        {
            //생성하고 초기화
            instanceResultPopup = Instantiate(resultPopup, gameObject.transform);
            instanceResultPopup.Initialize();
        }
        //생성되어있다면 활성화
        instanceResultPopup.Open();
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInGameResult : MonoBehaviour,IPopup
{
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button returnButton;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private GameSceneController sceneController;

    public void Initialize()
    {
        sceneController = SceneBase.Current as GameSceneController;
        restartButton.onClick.AddListener(Restart);
        returnButton.onClick.AddListener(Return);
    }

    public void Open()
    {
        (this as IPopup).Open();
        //점수 표시
        SetScoreText();
        //TODO:보상 표시
    }

    private void SetScoreText()
    {
        scoreText.text = $"{sceneController.Score}";
    }

    private void Restart()
    {
        //게임 다시 시작
        sceneController.ReStart();
    }

  
    private void Return()
    {
        //로비로 돌아가기
        sceneController.Return();
    }

}

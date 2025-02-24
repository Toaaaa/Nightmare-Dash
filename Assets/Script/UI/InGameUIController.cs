using TMPro;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    public HpBar HPBar;
    public TextMeshProUGUI ScoreText;

    private GameSceneController sceneController;


    private void Start()
    {
        sceneController = SceneBase.Current as GameSceneController;
        sceneController.OnChangedScoreAmountEvnet += UpdateScoreText;
    }

    public void Initialize()
    {
        // 일단 설정
        HPBar.SetHpBar(100);
        UpdateScoreText(0);
    }

    private void OnDestroy()
    {
        sceneController.OnChangedScoreAmountEvnet -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        ScoreText.text = $"Score : {score}";
    }
}

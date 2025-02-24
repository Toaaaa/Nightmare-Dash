using System;
using UnityEngine;

public class GameSceneController : SceneBase
{
    //점수
    public int Score
    {
        get => score;
        set
        {
            score = value;
            OnChangedScoreAmountEvnet(score);
        }
    }

    private int score = 0;

    //점수값이 변할때 발생하는 이벤트
    public Action<int> OnChangedScoreAmountEvnet = delegate { };

    [SerializeField]
    private InGameUIController uiController;

    protected override void OnStart(object data)
    {
        base.OnStart(data);
        uiController.Initialize();
    }
}

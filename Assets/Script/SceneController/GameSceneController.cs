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

    //게임이 끝났는지 판단
    public bool IsFinishGame
    {
        get => isFinishGame;
        set
        {
            isFinishGame = value;
            if (isFinishGame)
            {
                OnFinishGameEvent();
            }
        }
    }

    private bool isFinishGame = false;

    //게임이 끝났을때 호출되는 이벤트
    public Action OnFinishGameEvent = delegate { };

    public Hp GameHp { get; set; } = new();

    public InGameUIController uiController;
    [SerializeField] Player player;

    protected override void OnStart(object data)
    {
        base.OnStart(data);

        //체력 설정
        GameHp.SetHp(100);
        GameHp.OnChangedHpAmountEvent += FinishGame;

        uiController.Initialize();
    }

    private void OnDestroy()
    {
        GameHp.OnChangedHpAmountEvent -= FinishGame;
    }

    private void FinishGame(float current)
    {
        if (current == 0)
        {
            IsFinishGame = true;
        }
    }

    public void ReStart()
    {
        LoadScene("Game");
    }

    public void Return()
    {
        LoadScene("MainLobby");
    }

    public Vector2 GetPlayerPos()
    {
        return player.transform.position;
    }
    public void SetPlayerPos(Vector2 pos)
    {
        player.transform.position = pos;
    }
    public void ResetPlayer()
    {
        player.ResetP();
    }
}

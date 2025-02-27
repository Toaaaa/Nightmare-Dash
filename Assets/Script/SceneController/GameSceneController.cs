using System;
using UnityEngine;

public class GameSceneController : SceneBase
{
    //점수
    public float Score
    {
        get => score;
        set
        {
            score = value;
            OnChangedScoreAmountEvnet((int)score);
        }
    }

    private float score = 0;

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
    public InGameAchievement inGameAchievement;
    [SerializeField] Player player;

   

    private void Update()
    {
        GameHp.UpdateHp(player.GetCurrentHp());
    }

    protected override void OnStart(object data)
    {
        base.OnStart(data);
        GameHp.OnChangedHpAmountEvent += FinishGame;
        inGameAchievement.SetGameStart();// 인게임 업적 체킹 시작.
        GameHp.SetHp(100);
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
            GameHp.OnChangedHpAmountEvent -= FinishGame;
            DataManager.Instance.Coin.Add((long)score);
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

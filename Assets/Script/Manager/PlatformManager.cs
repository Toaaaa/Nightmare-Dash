using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameSceneController sceneController;
    // 변수
    [SerializeField] float speed = 7f; // 이동 속도 7에서 최대 12까지.
    [SerializeField] bool isGameStart = false; // 게임 시작 여부
    [SerializeField]
    Vector2 spawnPoint = new Vector2(0, 0);// 플레이어 초기 생성 위치.
    Vector2 gridPoint = new Vector2(0, 0); // 맵 타일 초기 위치.
    float time = 0; // 시간

    // 맵 그리드
    [SerializeField] Player player; // 플레이어
    [SerializeField] Transform mapGrid; // 맵 타일
    [SerializeField] List<Point> points;  // 코인
    [SerializeField] List<RunningItem> runningItems; // 피격시 사용되는 아이템 리스트

    private void Awake()
    {
        //spawnPoint = sceneController.GetPlayerPos();// 플레이어 초기 생성 위치.
        gridPoint = mapGrid.position; // 맵 타일 초기 위치.
        runningItems = new List<RunningItem>();
        foreach(var item in FindObjectsOfType<MonoBehaviour>())
        {
            if(item is RunningItem)
            {
                runningItems.Add(item as RunningItem);
            }
        }
    }
    void Update()
    {
        //진행 속도를 가져와서 그것에 맞춰서 다음 플렛폼 생성
        if(isGameStart)
        {
            mapGrid.transform.Translate(Vector3.left * speed * Time.deltaTime);
            SpeedUpGradual();
        }
        if(player.GetCurrentHp() <= 0)
        {
            StopGame();
            // 게임 오버 연출
        }
#if UNITY_EDITOR
        // 테스트 코드
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartGame();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            StopGame();
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            ResetGame();
        }
#endif
    }

    public void StartGame()
    {
        // 게임 + 플렛폼이 움직이기 시작.
        isGameStart = true;
        time = 0;
    }
    public void ResetGame()
    {
        isGameStart = false;// 게임 정지(플렛폼 정지)
        time = 0;
        player.ResetP();// 플레이어 데이터
        sceneController.SetPlayerPos(spawnPoint);// 플레이어 위치 초기화
        sceneController.ResetPlayer();// 플레이어 상태 초기화
        mapGrid.position = gridPoint; // 맵 타일 위치 초기화
        points.ForEach(p => p.PointAnimReset());// 코인 애니메이션 초기화
        runningItems.ForEach(item => item.ResetItemAnim());// 아이템 애니메이션 초기화
    }
    public void StopGame()
    {
        isGameStart = false;// 게임 정지(플렛폼 정지)
    }
    public float GetSpeed()// 플렛폼 속도.
    {
        return speed;
    }
    void SpeedUpGradual() // 점점 빨라지는 속도, 60초뒤 최대 속도 12도달 (4만큼 증가)
    {
        // 속도를 점점 빨라지게 하는 함수
        if(time < 60)
        {
            speed = Mathf.Min(12f, speed + (4f / 60f) * Time.deltaTime);
            time += Time.deltaTime;
        }
    }
}

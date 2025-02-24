using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // 변수
    [SerializeField] float speed = 8f; // 이동 속도 8에서 최대 12까지.
    [SerializeField] bool isGameStart = false; // 게임 시작 여부
    float time = 0; // 시간
    //[SerializeField] SpawnPoint spawnPoint; // 플랫폼 생성 위치

    // 맵 그리드
    [SerializeField] Transform mapGrid; // 맵 타일

    // 데이터 베이스
    [SerializeField] List<RunningItem> runningItems; // 피격시 사용되는 아이템 리스트


    void Start()
    {
        
    }
    void Update()
    {
        //진행 속도를 가져와서 그것에 맞춰서 다음 플렛폼 생성
        if(isGameStart)
        {
            mapGrid.transform.Translate(Vector3.left * speed * Time.deltaTime);
            SpeedUpGradual();
        }
    }

    public void StartGame()
    {
        // 게임 + 플렛폼이 움직이기 시작.
        isGameStart = true;
        time = 0;
    }
    public void ResetGame()
    {
        // 게임 + 플렛폼이 초기화
    }
    public void StopGame()
    {
        // 게임 + 플렛폼이 멈춤
        isGameStart = false;
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
            speed += Mathf.Min(12f, speed + (4f / 60f) * Time.deltaTime);
            time += Time.deltaTime;
        }
    }
    void InitalPlatform() // 초기 플랫폼 생성 (시작 플렛폼)
    {
        
    }
    void CreateRunningItem()
    {
        // 피격시 사용되는 아이템 생성
    }
}

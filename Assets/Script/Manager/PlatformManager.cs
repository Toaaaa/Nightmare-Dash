using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // 변수
    [SerializeField] float speed = 1.5f; // 이동 속도
    //[SerializeField] SpawnPoint spawnPoint; // 플랫폼 생성 위치

    // 이니셜 데이터 + 프리팹
    [SerializeField] GameObject startPlatform; // 시작 플랫폼
    [SerializeField] Platform platformPrefab; // 플랫폼 프리팹 (발판 1칸길이의 프리팹)



    // 데이터 베이스
    [SerializeField] List<RunningItem> runningItems; // 피격시 사용되는 아이템 리스트


    void Start()
    {
        
    }
    void Update()
    {
        //진행 속도를 가져와서 그것에 맞춰서 다음 플렛폼 생성
    }

    public void StartGame()
    {
        // 게임 + 플렛폼이 움직이기 시작.
    }
    public void ResetGame()
    {
        // 게임 + 플렛폼이 초기화
    }
    public void StopGame()
    {
        // 게임 + 플렛폼이 멈춤
    }


    public float GetSpeed()// 플렛폼 속도.
    {
        return speed;
    }

    void InitalPlatform() // 초기 플랫폼 생성 (시작 플렛폼)
    {
        
    }
    void CreateRunningItem()
    {
        // 피격시 사용되는 아이템 생성
    }
}

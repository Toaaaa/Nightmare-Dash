using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] float speed = 1.5f; // 이동 속도 + 다음 플렛폼 생성시간 (임시로 플랫폼 매니저에서 관리)
    [SerializeField] Platform startPlatform; // 시작 플랫폼
    [SerializeField] Platform platformPrefab; // 플랫폼 프리팹
    [SerializeField] List<Obstacle> obstacles; // 장애물 리스트
    [SerializeField] List<RunningItem> runningItems; // 피격시 사용되는 아이템 리스트

    // 1. 플렛폼은 랜덤 생성 (ground level의 기본 플렛폼과 공중 플렛폼 존재)
    // 2. 장애물은 플렛폼의 중간 일정 비율 범위 내에서만 생성되며, 플렛폼의 길이에 따라 갯수가 결정된다.

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


    void InitalPlatform() // 초기 플랫폼 생성 (시작 플렛폼 뒤로 3개)
    {
        
    }
    void CreatePlatform()
    {
        // 플랫폼 생성
    }
    void CreateObstacle()
    {
        // 장애물 생성
    }
    void CreateRunningItem()
    {
        // 피격시 사용되는 아이템 생성
    }
}

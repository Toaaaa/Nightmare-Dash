using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] float speed = 1.5f; // �̵� �ӵ� + ���� �÷��� �����ð� (�ӽ÷� �÷��� �Ŵ������� ����)
    [SerializeField] Platform startPlatform; // ���� �÷���
    [SerializeField] Platform platformPrefab; // �÷��� ������
    [SerializeField] List<Obstacle> obstacles; // ��ֹ� ����Ʈ
    [SerializeField] List<RunningItem> runningItems; // �ǰݽ� ���Ǵ� ������ ����Ʈ

    // 1. �÷����� ���� ���� (ground level�� �⺻ �÷����� ���� �÷��� ����)
    // 2. ��ֹ��� �÷����� �߰� ���� ���� ���� �������� �����Ǹ�, �÷����� ���̿� ���� ������ �����ȴ�.

    void Start()
    {
        
    }
    void Update()
    {
        //���� �ӵ��� �����ͼ� �װͿ� ���缭 ���� �÷��� ����
    }

    public void StartGame()
    {
        // ���� + �÷����� �����̱� ����.
    }
    public void ResetGame()
    {
        // ���� + �÷����� �ʱ�ȭ
    }
    public void StopGame()
    {
        // ���� + �÷����� ����
    }


    void InitalPlatform() // �ʱ� �÷��� ���� (���� �÷��� �ڷ� 3��)
    {
        
    }
    void CreatePlatform()
    {
        // �÷��� ����
    }
    void CreateObstacle()
    {
        // ��ֹ� ����
    }
    void CreateRunningItem()
    {
        // �ǰݽ� ���Ǵ� ������ ����
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // ����
    [SerializeField] float speed = 1.5f; // �̵� �ӵ�
    //[SerializeField] SpawnPoint spawnPoint; // �÷��� ���� ��ġ

    // �̴ϼ� ������ + ������
    [SerializeField] GameObject startPlatform; // ���� �÷���
    [SerializeField] Platform platformPrefab; // �÷��� ������ (���� 1ĭ������ ������)


    // ������ ���̽�
    [SerializeField] List<RunningItem> runningItems; // �ǰݽ� ���Ǵ� ������ ����Ʈ


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


    public float GetSpeed()// �÷��� �ӵ�.
    {
        return speed;
    }

    void InitalPlatform() // �ʱ� �÷��� ���� (���� �÷���)
    {
        
    }
    void CreateRunningItem()
    {
        // �ǰݽ� ���Ǵ� ������ ����
    }
}

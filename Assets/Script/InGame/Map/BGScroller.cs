using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BGScroller : MonoBehaviour
{
    [SerializeField] private GameObject[] backGround;
    public float Speed = 1;
    public float maxSpeed = 100;
    public float Acceleration = 1.1f;
    private float backgroundWidth;
    private float currentSpeed;


    public void Start()
    {
        currentSpeed = Speed;
        backgroundWidth = backGround[0].GetComponent<BoxCollider2D>().size.x;
    }

    public void Update()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += Acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }

        // 모든 배경을 왼쪽으로 이동
        foreach (GameObject bg in backGround)
        {
            bg.transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        }
        // 배경 재배치 확인
        foreach (GameObject bg in backGround)
        {
            if (bg.transform.position.x < -backgroundWidth)
            {
                RepositionBackground(bg);
            }
        }

    }
    void RepositionBackground(GameObject bg)
    {
        // 가장 오른쪽 배경을 찾음
        float maxX = float.MinValue;
        foreach (GameObject other in backGround)
        {
            if (other.transform.position.x > maxX)
            {
                maxX = other.transform.position.x;
            }
        }

        // 가장 오른쪽 배경 뒤로 이동
        bg.transform.position = new Vector3(maxX + backgroundWidth, bg.transform.position.y, bg.transform.position.z);
    }
}


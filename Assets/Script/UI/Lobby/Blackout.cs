using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Blackout : MonoBehaviour
{
    public Light2D[] lights; // 씬에 있는 모든 라이트
    public float blackoutDuration = 2f; // 조명이 꺼지는 시간
    private float[] originalIntensities; // 각 조명의 원래 밝기 저장

    private void Start()
    {
        // 원래 밝기 저장
        originalIntensities = new float[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            originalIntensities[i] = lights[i].intensity;
        }

        StartCoroutine(BlackoutEffect());
    }

    private IEnumerator BlackoutEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 30)); // 10~20초마다 랜덤으로 발생

            // 모든 조명 끄기
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].intensity = 0;
            }

            yield return new WaitForSeconds(blackoutDuration);

            // 원래 밝기로 복구
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].intensity = originalIntensities[i];
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowEffect : MonoBehaviour
{
    public Light2D flashLight;  // 순간적으로 밝아지는 조명
    public SpriteRenderer shadowFigure; // 그림자 실루엣

    public float minDelay = 5f;
    public float maxDelay = 15f;
    public float flashDuration = 0.3f;

    private void Start()
    {
        shadowFigure.enabled = false;
        StartCoroutine(FlashShadow());
    }

    private IEnumerator FlashShadow()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            // 그림자와 조명을 켜기
            shadowFigure.enabled = true;
            flashLight.intensity = 2;

            yield return new WaitForSeconds(flashDuration);

            // 다시 끄기
            shadowFigure.enabled = false;
            flashLight.intensity = 0;
        }
    }
}

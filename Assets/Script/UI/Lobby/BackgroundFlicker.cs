using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BackgroundFlicker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private Image panelImage;
    private Color originalColor;

    public float minAlpha = 0.3f;  // 최소 밝기 (0 = 완전 어두움)
    public float maxAlpha = 1.0f;  // 최대 밝기 (1 = 완전 밝음)
    public float flickerSpeed = 0.1f;  // 깜박이는 속도
    public bool isFlickering = true;  // 깜박임 활성화 여부

    private void Start()
    {
        // 스프라이트 렌더러 또는 UI 이미지 찾기
        spriteRenderer = GetComponent<SpriteRenderer>();
        panelImage = GetComponent<Image>();
        light2D = GetComponent<Light2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
        else if (panelImage != null)
            originalColor = panelImage.color;

        StartCoroutine(FlickerBackground());
    }

    private IEnumerator FlickerBackground()
    {
        while (isFlickering)
        {
            float randomAlpha = Random.Range(minAlpha, maxAlpha);
            Color newColor = originalColor;
            newColor.a = randomAlpha;

            if (spriteRenderer != null)
                spriteRenderer.color = newColor;
            else if (panelImage != null)
                panelImage.color = newColor;

            yield return new WaitForSeconds(Random.Range(0.05f, flickerSpeed));
        }
    }

    // 외부에서 제어 가능
    public void SetFlickering(bool state)
    {
        isFlickering = state;
        if (!state)
        {
            if (spriteRenderer != null)
                spriteRenderer.color = originalColor;
            else if (panelImage != null)
                panelImage.color = originalColor;
           
        }
    }
}


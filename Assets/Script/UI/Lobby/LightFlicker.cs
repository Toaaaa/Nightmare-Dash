using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Light2D 사용

public class LightFlicker : MonoBehaviour
{
    public Light2D light2D; // 2D 라이트
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.1f; // 깜박이는 속도
    public bool isFlickering = true;

    private void Start()
    {
        if (light2D == null) light2D = GetComponent<Light2D>();
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (isFlickering)
        {
            light2D.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(0.05f, flickerSpeed));
        }
    }

    // 외부에서 제어 가능
    public void SetFlickering(bool state)
    {
        isFlickering = state;
        if (!state) light2D.intensity = maxIntensity; // 원래 밝기로 복구
    }
}

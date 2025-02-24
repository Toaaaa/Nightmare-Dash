using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GlitchEffect : MonoBehaviour
{
    public Material glitchMaterial; // 쉐이더 머티리얼
    private float glitchStrength = 0f;

    private void Update()
    {
        // 특정 조건에서 글리치 효과를 활성화
        if (Random.value > 0.95f) // 랜덤 확률로 효과 발생
        {
            glitchStrength = Random.Range(0.05f, 0.2f);
            Invoke("ResetGlitch", 0.1f); // 0.1초 후 리셋
        }

        glitchMaterial.SetFloat("_GlitchStrength", glitchStrength);
    }

    private void ResetGlitch()
    {
        glitchStrength = 0f;
    }
}



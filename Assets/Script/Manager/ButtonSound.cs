using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private static AudioSource audioSource; // 버튼 사운드를 재생할 AudioSource
    public AudioClip clickSound;    // 버튼 클릭 효과음

    private void Awake()
    {
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("ButtonAudioSource");
            audioSource = audioObj.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioObj);
        }
    }

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayClickSound);
        }
    }

    void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}

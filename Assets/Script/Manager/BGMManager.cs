using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource audioSource;
    private BGMManager bgmManager;

    public AudioClip titleAndLobbyBGM;
    public AudioClip gameBGM;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoded;
        bgmManager = FindObjectOfType<BGMManager>();

        PlayBGMForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name);
    }

    void PlayBGM(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying) return;

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void PlayBGMForScene(string sceneName)
    {
        if (bgmManager == null) return;

        if (sceneName == "Title" || sceneName == "Lobby")
        {
            bgmManager.PlayBGM(bgmManager.titleAndLobbyBGM);
        }
        else
        {
            bgmManager.PlayBGM(bgmManager.gameBGM);
        }

    }
}

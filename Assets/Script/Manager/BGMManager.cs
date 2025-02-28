using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    public AudioSource audioSource;

    public AudioClip titleAndLobbyBGM;
    public AudioClip gameBGM;
    public AudioClip gachaBGM;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.loop = true;
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
        audioSource.Play();
    }

    //게임BGM재생
    public void PlayGameBGM()
    {
        PlayBGM(gameBGM);
    }

    void PlayBGMForScene(string sceneName)
    {
        //튜토리얼 BGM은 따로 튜토리얼 씬에서 재생
        if (sceneName == "Tutorial")
        {
            //재생하고있는 음악 정지
            audioSource.Stop();
            return;
        }

        if (sceneName == "Title" || sceneName == "MainLobby")
        {
            if (audioSource.clip != titleAndLobbyBGM)
            {
                PlayBGM(titleAndLobbyBGM);
            }
        }

        else if (sceneName == "Gacha")
        {
            if(audioSource.clip != gachaBGM)
            {
                PlayBGM(gachaBGM);
            }
        }

        else
        {

            if (audioSource.clip != gameBGM)
            {
                PlayBGM(gameBGM);
            }
        }
    }
}

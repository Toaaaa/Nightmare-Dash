using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GhostManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] ghostImages;

    [SerializeField]
    private Image ghostImage;

    [SerializeField]
    private AudioSource audioSource;

    private Coroutine showGhostCoroutine; //귀신 출현 코루틴

    public float ShowGhostCoolTime = 60.0f; // 귀신 출현 쿨타임
    public float GhostImageAlpha = 0.85f;   // 귀신 이미지 투명도

    public float FadeInDuration = 0.1f;
    public float FadeOutDuration = 0.7f;

    private void Start()
    {
        SceneManager.sceneLoaded += ShowGhost;
        showGhostCoroutine = StartCoroutine(IShowGhost());
    }

    private void ShowGhost(Scene scene, LoadSceneMode mode)
    {
        //도중에 귀신 안나오게 씬 로드하면 코루틴 일단 정지
        if (showGhostCoroutine != null)
        {
            StopCoroutine(showGhostCoroutine);
            showGhostCoroutine = null;
        }
        //튜토리얼이나 게임 씬에서는 귀신 출현 안하게
        if (scene.name == "Tutorial" && scene.name == "Game")
        {
            return;
        }
        showGhostCoroutine = StartCoroutine(IShowGhost());
    }

    //귀신 출현
    private IEnumerator IShowGhost()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(ShowGhostCoolTime);
            Debug.Log("귀신 출현!");
            ghostImage.sprite = GetRandomGhostSprite(); // 랜덤 귀신 이미지 불러오기
            yield return IFadeIn(); // 귀신 이미지 서서히 나타나게
            audioSource.PlayOneShot(audioSource.clip); //효과음 한번 재생
            yield return new WaitForSecondsRealtime(2); // 2초 대기
            yield return IFadeOut(); // 귀신 이미지 서서히 사라지게
        }
    }

    //랜덤으로 귀신 이미지 불러오기
    private Sprite GetRandomGhostSprite()
    {
        int index = Random.Range(0, ghostImages.Length);
        return ghostImages[index];
    }

    private IEnumerator IFadeIn()
    {
        float elapsedTime = 0f;
        Color color = ghostImage.color;
        color.a = 0f;
        ghostImage.color = color;
        ghostImage.gameObject.SetActive(true);

        while (elapsedTime < FadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, GhostImageAlpha, elapsedTime / FadeInDuration);
            ghostImage.color = color;
            yield return null;
        }

        color.a = GhostImageAlpha;
        ghostImage.color = color;
    }

    private IEnumerator IFadeOut()
    {
        float elapsedTime = 0f;
        Color color = ghostImage.color;
        color.a = GhostImageAlpha;
        ghostImage.color = color;

        while (elapsedTime < FadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(GhostImageAlpha, 0f, elapsedTime / FadeOutDuration);
            ghostImage.color = color;
            yield return null;
        }

        color.a = 0f;
        ghostImage.color = color;
        ghostImage.gameObject.SetActive(false);
    }

    public void EncountGhosst()
    {
        AchievementManager.Instance.UnlockAchievement("공포 속으로");
    }
}



using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TutorialSceneController : SceneBase
{
    private string story = "여기가 어딘지 얼마나 달렸는지 모르겠다. " +
        "\n그저 뒤따라오는 무언가에 잡히지 않아야겠다는 " +
        "\n본능만이 머릿속에 가득하다...";

    [SerializeField]
    private TextMeshProUGUI storyText;
    [SerializeField]
    private PlayableDirector storyEffect;
    [SerializeField]
    private Image fadePanel;
    [SerializeField]
    private Player player;
    [SerializeField]
    private PlatformManager platformManager;

    protected override void OnStart(object data)
    {
        base.OnStart(data);
        //초기 비활성 상태
        player.gameObject.SetActive(false);
        platformManager.gameObject.SetActive(false);
        //스토리 소개 연출 재생
        storyEffect.Play();
        Debug.Log("호출");
    }

    //튜토리얼 시작 (Timeline에서 호출)
    public void StartTutorial()
    {
        player.gameObject.SetActive(true);
        platformManager.gameObject.SetActive(true);
        //BGM재생
        BGMManager.instance.PlayGameBGM();
    }

    //타이핑 효과 재생 (Timeline에서 호출)
    public void PlayTextEffect()
    {
        StartCoroutine(IPlayTextEffect());
    }

    //텍스트 타이핑 효과 코루틴
    public IEnumerator IPlayTextEffect()
    {
        storyText.text = string.Empty;

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < story.Length; i++)
        {
            sb.Append(story[i]);
            storyText.text = sb.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    //튜토리얼 종료
    public void EndTutorial()
    {
        PlayerPrefs.SetInt("TutorialClear", 1);
        PlayerPrefs.Save();
        StartCoroutine(IFadeIn());
    }

    //페이드 인 재생
    private IEnumerator IFadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadePanel.color;
        color.a = 0f;
        fadePanel.color = color;
        fadePanel.gameObject.SetActive(true);

        while (elapsedTime < 1.0f)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / 1.0f);
            fadePanel.color = color;
            yield return null;
        }

        color.a = 1f;
        fadePanel.color = color;

        //로비로 이동
        LoadScene("MainLobby");
    }
}

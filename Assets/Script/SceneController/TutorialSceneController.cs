using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialSceneController : SceneBase
{
    private string story = "여기가 어딘지 얼마나 달렸는지 모르겠다. " +
        "\n그저 뒤따라오는 무언가에 잡히지 않아야겠다는 " +
        "\n본능만이 머릿속에 가득하다...";

    [SerializeField]
    private TextMeshProUGUI storyText;

    [SerializeField]
    private PlayableDirector storyEffect;

    protected override void OnStart(object data)
    {
        base.OnStart(data);
        //storyEffect.Play();
    }

    public void PlayTextEffect()
    {
        StartCoroutine(IPlayTextEffect());
    }

    public IEnumerator IPlayTextEffect()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < story.Length; i++)
        {
            sb.Append(story[i]);
            storyText.text = sb.ToString();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void StartTutorial()
    {

    }
}

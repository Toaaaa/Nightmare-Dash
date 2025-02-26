using System.Collections;
using TMPro;
using UnityEngine;


public partial class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance => instance;

    private static TutorialManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public TextMeshProUGUI GuideText;

    public KeyCode requiredKey = KeyCode.Space; // 상호작용 키

    public bool IsClearTutorial = false;

    public bool IsInTutorial = false;


    public void Update()
    {
        if (IsInTutorial && Input.GetKeyDown(requiredKey))
        {
            IsClearTutorial = true;
        }
    }

    public void PlayTutorial(TutorialType type)
    {
        requiredKey = GetKey(type);
        StartCoroutine(ISlowDown(type));
    }
    private KeyCode GetKey(TutorialType type)
    {
        switch (type)
        {
            case TutorialType.Jump:
                return requiredKey = KeyCode.Space;
            case TutorialType.Roll:
                return requiredKey = KeyCode.S;
        }
        return KeyCode.None;
    }

    // 서서히 느려지면서 튜토리얼 텍스트 표시
    public IEnumerator ISlowDown(TutorialType type)
    {
        while (Time.timeScale > 0.02f)
        {
            Time.timeScale -= Time.deltaTime * 1.5f;  // 서서히 느려짐
            yield return null;
        }
        Time.timeScale = 0f;  // 완전히 멈춤
        Instance.MoveNextTutorial(type);
        Instance.IsInTutorial = true;
        StartCoroutine(WaitForTutorialClear(type));
    }

    //튜토리얼 클리어 후 원래대로 돌리는 함수
    private IEnumerator WaitForTutorialClear(TutorialType type)
    {
        yield return new WaitUntil(() => IsClearTutorial);
        GuideText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}



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

    public TextMeshProUGUI GuideText; // 튜토리얼 가이트 텍스트

    public KeyCode requiredKey = KeyCode.Space; // 상호작용 키

    public bool IsClearTutorial = false;  // 각 튜토리얼을 클리어 했는지

    public bool IsInTutorial = false;  // 튜토리얼 중인지


    public void Update()
    {
        //튜토리얼중일때 조건키를 누르면
        if (IsInTutorial && Input.GetKeyDown(requiredKey))
        {
            //튜토리얼 클리어
            IsClearTutorial = true;
        }
    }

    //튜토리얼 재생
    public void PlayTutorial(TutorialType type)
    {
        requiredKey = GetKey(type);
        StartCoroutine(ISlowDown(type));
    }

    //튜토리얼 클리어 조건 키 반환
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
        while (Time.timeScale > 0.01f)
        {
            Time.timeScale -= Time.deltaTime * 1.5f;  // 서서히 느려짐
            yield return null;
        }
        Time.timeScale = 0f;  // 완전히 멈춤
        Instance.MoveNextTutorial(type); // 지정 튜토리얼로 이동
        Instance.IsInTutorial = true; // 튜토리얼 하는 중
        StartCoroutine(WaitForTutorialClear(type));
    }

    //튜토리얼 클리어 후 원래대로 초기화
    private IEnumerator WaitForTutorialClear(TutorialType type)
    {
        yield return new WaitUntil(() => IsClearTutorial);
        GuideText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}



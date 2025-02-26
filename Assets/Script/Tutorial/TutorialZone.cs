using UnityEngine;
using static TutorialManager;

public class TutorialZone : MonoBehaviour
{
    [SerializeField]
    private TutorialType stateType = TutorialType.None; // 튜토리얼 타입

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (stateType == TutorialType.None)
            {
                Debug.Log("튜토리얼 타입을 설정해주세요!");
                return;
            }
            if (stateType == TutorialType.End)
            {
                (SceneBase.Current as TutorialSceneController).EndTutorial();
                return;
            }
            // 튜토리얼 재생
            TutorialManager.Instance.PlayTutorial(stateType);
        }
    }
}

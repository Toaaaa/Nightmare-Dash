using TMPro;
using UnityEngine;

public partial class TutorialManager
{
    public class Tutorial_Jump : TutorialStateBase
    {
        public override TutorialType Type => TutorialType.Jump; // 튜토리얼 타입

        public override void OnEnter()
        {
            PlayJumpTutorial();
        }

        // 점프 튜토리얼 시작
        private void PlayJumpTutorial()
        {
            Instance.GuideText.gameObject.SetActive(true);
            Instance.GuideText.text = "스페이스를 눌러서 점프하세요!";
        }
    }
}


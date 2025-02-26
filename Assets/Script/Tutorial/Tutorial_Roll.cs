using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class TutorialManager
{
    public class Tutorial_Roll : TutorialStateBase
    {
        public override TutorialType Type => TutorialType.Roll; // 튜토리얼 타입

        public override void OnEnter()
        {
            PlayRollTutorial();
        }

        // 구르기 튜토리얼 시작
        private void PlayRollTutorial()
        {
            Instance.GuideText.gameObject.SetActive(true);
            Instance.GuideText.text = "S를 눌러서 구르세요!";
        }
    }

}


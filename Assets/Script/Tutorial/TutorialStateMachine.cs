using System.Collections.Generic;

public partial class TutorialManager
{
    public enum TutorialType
    {
        None,
        Jump, // 점프
        Roll, // 구르기
        End
    }

    public TutorialStateBase Current
    {
        get => current;
        set
        {
            current.OnExit();
            current = value;
            current.OnEnter();
        }
    }

    private TutorialStateBase current = new Tutorial_None();


    public Dictionary<TutorialType, TutorialStateBase> NextTutorialDic()
    {
        return new Dictionary<TutorialType, TutorialStateBase>()
        {
            { TutorialType.Jump,  new Tutorial_Jump() },
            { TutorialType.Roll,  new Tutorial_Roll() },
        };
    }

    //지정 튜토리얼로 이동 
    public void MoveNextTutorial(TutorialType type)
    {
        Current = NextTutorialDic()[type];
    }
}

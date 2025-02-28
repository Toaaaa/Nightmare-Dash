public partial class TutorialManager
{
    public abstract class TutorialStateBase
    {
        public abstract TutorialType Type { get; }
        public virtual void OnEnter() { }
        public virtual void OnExit()
        {
            Instance.IsInTutorial = false;
            Instance.IsClearTutorial = false;
        }
    }

    public class Tutorial_None : TutorialStateBase
    {
        public override TutorialType Type => TutorialType.None;

        public override void OnEnter() { }

        public override void OnExit() { }
    }
}

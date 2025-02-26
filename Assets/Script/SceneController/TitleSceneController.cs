using UnityEngine;

public class TitleSceneController : SceneBase
{
    protected override void OnStart(object data)
    {
        base.OnStart(data);
    }

    public void OnClickStart()
    {
        //튜토리얼을 클리어했다면
        if (PlayerPrefs.HasKey("TutorialClear"))
        {
            //로비로 이동
            LoadScene("MainLobby");
        }
        //클리어 하지않았다면
        else
        {
            //튜토리얼로 이동
            LoadScene("Tutorial");
        }
    }
}

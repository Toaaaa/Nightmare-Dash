using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController : SceneBase
{
    public void OnClickStart()
    {
        LoadScene("Tutorial");
    }

    public void ClickStart()
    {
        LoadScene("MainLobby");
    }

    protected override void OnStart(object data)
    {
        base.OnStart(data);
    }
}

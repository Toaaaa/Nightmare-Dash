using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAchievement : MonoBehaviour
{
    bool gameStart = false;
    float time = 0;

    //퀘스트 변수
    bool Hit30Sec = false;//30초 이상 버티기 true 30초이내에 피격 당한것으로 판단.

    private void Update()
    {
        if (gameStart)
        {
            time += Time.deltaTime;
            AchievementNoHit30Sec();
        }
    }
    public void SetGameStart()
    {
        gameStart = true;
        time = 0;
    }
    public void SetNoHit30Sec(bool b)
    {
        Hit30Sec = b;
    }

    void AchievementNoHit30Sec()
    {
        if (time >= 30 && !Hit30Sec)
        {
            //AchievementManager.Instance.UnlockAchievement("30초 동안 피격 없이 달리기");
        }
    }

}

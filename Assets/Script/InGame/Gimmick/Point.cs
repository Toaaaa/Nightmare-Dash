using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class Point : MonoBehaviour //타일을 달리면서 획득하는 점수
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")// 플레이어와 충돌시
        {
            if(SceneBase.Current is GameSceneController gameScene)
            {
                gameScene.Score += 1;// 점수 획득
                GetComponent<Animator>().SetTrigger("GetCoin");// 코인 획득 애니메이션 재생
            }
        }
    }

    public void PointAnimReset()
    {
        // 획득시 재생되었던 애니메이션 리셋
        GetComponent<Animator>().SetTrigger("ResetCoin");
    }
}

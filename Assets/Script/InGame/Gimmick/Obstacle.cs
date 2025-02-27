using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))// 플레이어의 하위 오브젝트, 피격판정 콜라이더에 Player 태그를 부여
        {
            collision.GetComponentInParent<Player>().GetDmg(20);
            GameSceneController gc = SceneBase.Current as GameSceneController;
            gc.inGameAchievement.SetNoHit30Sec(true);
        }
    }
}

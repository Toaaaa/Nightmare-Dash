using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJumpItem : MonoBehaviour, RunningItem
{
    public void ApplyItemEffect(Player player)
    {
        player.TripleJumpActivate();// 트리플 점프 활성화
        GetComponent<Animator>().SetTrigger("GetItem");// 아이템 획득 애니메이션 재생
    }

    public void ResetItemAnim()
    {
        GetComponent<Animator>().SetTrigger("ResetItem");// 아이템 획득 애니메이션 리셋
    }
}

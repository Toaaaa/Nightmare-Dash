using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    //기본 스탯
    float ori_MaxHp = 100f;// 기본 체력
    float ori_score = 1f;// 기본 재화 획득량 (코인 획득시마다 얻는 점수에 곱해진다)
    float ori_InvincibleTime = 5f;// 기본 무적시간

    //유물로 인한 추가 스탯
    float add_MaxHp;// 추가 체력
    float add_score;// 추가 재화 획득량
    float add_InvincibleTime;// 추가 무적시간

    public float GetTotalHp()
    {
        return ori_MaxHp + add_MaxHp;
    }
    public float GetTotalScoreValue()
    {
        return ori_score + add_score;
    }
    public float GetTotalInvincibleTime()
    {
        return ori_InvincibleTime + add_InvincibleTime;
    }

    public void SetAddMaxHp(float value)
    {
        add_MaxHp = value;
    }
    public void SetAddScore(float value)
    {
        add_score = value;
    }
    public void SetAddInvincibleTime(float value)
    {
        add_InvincibleTime = value;
    }
    public void ResetAddStats()// 게임 초기화등 유물의 능력치를 초기화할때 사용.
    {
        add_MaxHp = 0;
        add_score = 0;
        add_InvincibleTime = 0;
    }
}

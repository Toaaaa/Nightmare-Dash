using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenItem : MonoBehaviour, RunningItem
{
    [SerializeField] float regenAmount = 10f;// 체력 회복량

    public void ApplyItemEffect(Player player)
    {
        player.Heal(regenAmount);
        GetComponent<Animator>().SetTrigger("GetItem");
    }

    public void ResetItemAnim()
    {
        GetComponent<Animator>().SetTrigger("ResetItem");
    }
}

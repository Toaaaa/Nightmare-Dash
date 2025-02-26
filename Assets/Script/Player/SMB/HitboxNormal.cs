using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNormal : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Player>().StartCoroutine(HitboxChangeDelay(animator));
    }

    IEnumerator HitboxChangeDelay(Animator animator)
    {
        yield return new WaitForSeconds(0.25f);// 구르기 피격판정 시간 연장.
        animator.GetComponent<Player>().HitboxSet(0);
    }
}

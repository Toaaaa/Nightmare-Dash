using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxNormal : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Player>().HitboxSet(0);
    }
}

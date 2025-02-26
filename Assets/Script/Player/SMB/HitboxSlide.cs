using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSlide : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Player>().HitboxSet(1);
    }
}

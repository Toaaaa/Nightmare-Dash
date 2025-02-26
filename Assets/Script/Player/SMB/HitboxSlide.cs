using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HitboxSlide : StateMachineBehaviour
{

    CancellationTokenSource cts;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(cts != null)
            cts.Cancel();//만약 delay 도중 다시 슬라이드 상태로 진입할 경우, 기존의 유니태스크 취소.
        animator.GetComponent<Player>().HitboxSet(1);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cts = new CancellationTokenSource();
        HitboxChangeDelay(animator, cts.Token).Forget();
    }
    private async UniTask HitboxChangeDelay(Animator animator, CancellationToken token)
    {
        try
        {
            Debug.Log("히트박스 변경");
            await UniTask.Delay(250, cancellationToken: token);
            animator.GetComponent<Player>().HitboxSet(0);
        }
        catch (OperationCanceledException) { Debug.Log("토큰 취소"); }
        finally { cts?.Dispose(); cts = null; }
    }
}

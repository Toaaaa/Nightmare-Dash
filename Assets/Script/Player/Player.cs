using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] float jumpHeight = 1.7f;
    [SerializeField] float jumpDuration = 0.7f;
    [SerializeField] float gravScale = 4f;
    float groundY;

    bool isDead = false;
    bool isJumping = false;
    float doubleJumpDelay = 0.05f;
    bool isOnGround = false;
    bool isSliding = false;


    private void Awake()
    {
        groundY = transform.position.y;
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(!isDead && isOnGround)
        {
            if (!isSliding)// 달리기
            {
                animator.SetBool("isSliding", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))// 점프
            {
                Jump();
            }

            if(Input.GetKeyDown(KeyCode.S))// 슬라이딩
            {
                Slide();
            }
        }
        if (isJumping)// 더블 점프
        {
            doubleJumpDelay -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) && doubleJumpDelay <= 0)
            {
                DoubleJump();
                doubleJumpDelay = 0.05f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isJumping = false;
            rb.gravityScale = 0;
            transform.DOKill();// 바닥에 닿으면 DOTween 중단
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            new WaitForSeconds(0.1f);// 코요테 타임 추가
            isOnGround = false;
        }
    }

    public void SetIsdead(bool isdead)
    {
        isDead = isdead;
    }

    void Slide()
    {
        if(!isDead)
        {
            isSliding = true;
            animator.SetBool("isSliding", true);
        }
    }
    void Jump()
    {
        if (isJumping) return;

        isJumping = true;
        isOnGround = false;
        animator.SetTrigger("Jump");

        float targetY = transform.position.y + jumpHeight;

        transform.DOMoveY(targetY, jumpDuration / 2)// 상승
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOMoveY(groundY, jumpDuration * 0.6f)// 하락
                .SetEase(Ease.InQuad)
                .OnUpdate(() =>
                {
                    // 땅과 충돌하면 DOTween 중단
                    if (isOnGround)
                    {
                        transform.DOKill();
                    }
                });
            });
    }
    void DoubleJump()
    {
        if (!isJumping) return;

        animator.SetTrigger("DoubleJump");

        float targetY = transform.position.y + jumpHeight;

        transform.DOKill();// Jump()에서 사용한 DOTween을 취소

        transform.DOMoveY(targetY, jumpDuration / 2)// 상승
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOMoveY(groundY, jumpDuration* 0.6f)// 하락
                .SetEase(Ease.InQuad)
                .OnUpdate(() =>
                {
                    // 땅과 충돌하면 DOTween 중단
                    if (isOnGround)
                    {
                        transform.DOKill();
                    }
                });
            });
    }
}

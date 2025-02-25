using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float jumpHeight = 5f;
    public float jumpDuration = 0.5f;
    bool isDead = false;

    bool isJumping = false;
    float doubleJumpDelay = 0.2f;
    bool isOnGround = false;
    bool isSliding = false;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if(!isDead && isOnGround)
        {
            if (!isSliding)// 달리기
            {
                isJumping = false;
                doubleJumpDelay = 0.2f;
                animator.SetBool("isSliding", false);
            }
            if (Input.GetKeyDown(KeyCode.Space))// 점프
            {
                Jump();
                isJumping = true;
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
                Jump();
                animator.SetTrigger("DoubleJump");
                isJumping = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.CompareTag("Ground"))
            isOnGround = true;
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
        transform.DOJump(transform.position, jumpHeight, 1, jumpDuration)
            .SetEase(Ease.OutQuad);
    }
}

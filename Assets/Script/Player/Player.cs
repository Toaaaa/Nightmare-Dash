using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] float jumpForce = 7f; // 점프 힘
    [SerializeField] int maxJumps = 2; // 최대 점프 횟수
    int jumpCount = 0;
    float slopeSpeed = 1.13f; // 경사면 속도
    bool isOnSlope = false;// 경사면 위에 있는지
    bool isOnGround = false;// 지면 위에 있는지
    bool isDead = false;
    bool fall = false;
    float coyoteTime = 0.1f; // 코요테 타임 (땅에서 살짝 벗어나도 점프 가능)
    float coyoteTimeCounter;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Update()
    {
        if (!isDead)
        {
            if (isOnGround)
            {
                if(Input.GetKey(KeyCode.S)) // 슬라이딩
                {
                    Slide();
                }
                else // 기본 달리기
                {
                    animator.SetBool("isRunning", true);
                    animator.SetBool("isSliding", false);
                    animator.ResetTrigger("DoubleJump");
                }

                coyoteTimeCounter = coyoteTime; // 땅에 있을 때 코요테 타임 리셋
                jumpCount = 0;
            }
            else // 코요테 타임 카운트
            {
                coyoteTimeCounter -= Time.deltaTime;
                animator.SetBool("isRunning", false);
                animator.SetBool("isSliding", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && (jumpCount < maxJumps || coyoteTimeCounter > 0) &&!isOnSlope) // 경사로에서는 점프 불가능
            {
                jumpCount++;
                Jump();
            }
        }
    }
    private void FixedUpdate()
    {
        if (isOnSlope)
        {
            rb.velocity = new Vector2(slopeSpeed, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (fall) return;
        rb.velocity = new Vector2(rb.velocity.x, 0f); // 기존 점프 속도 초기화 (더블 점프 시 중요)
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger(jumpCount == 1 ? "Jump" : "DoubleJump");
        animator.SetBool("isRunning", false);

        isOnGround = false;
        coyoteTimeCounter = 0; // 점프 시 코요테 타임 리셋
    }

    void Slide()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isSliding", true);
    }
    public void SetFall()
    {
        fall = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 법선 벡터(normal)가 위쪽(0, 1)에 가까운 경우에만 "땅"으로 인정
                if (contact.normal.y > 0.7f)
                {
                    isOnGround = true;
                    return;
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("UnderGround"))
        {
            fall = true;
        }
        if (collision.gameObject.CompareTag("Hill"))
        {
            isOnSlope = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hill"))
        {
            isOnSlope = false;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}

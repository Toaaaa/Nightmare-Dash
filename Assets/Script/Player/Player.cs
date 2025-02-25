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
    bool isGrounded = false;
    bool isSliding = false;
    bool isDead = false;
    bool fall = false;
    float coyoteTime = 0.1f; // 코요테 타임 (땅에서 살짝 벗어나도 점프 가능)
    float coyoteTimeCounter;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (!isDead)
        {
            if (isGrounded)
            {
                coyoteTimeCounter = coyoteTime; // 땅에 있을 때 코요테 타임 리셋
                jumpCount = 0;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) && (jumpCount < maxJumps || coyoteTimeCounter > 0))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.S) && isGrounded) // 슬라이딩
            {
                Slide();
            }
        }
    }

    void Jump()
    {
        if (fall) return;
        rb.velocity = new Vector2(rb.velocity.x, 0f); // 기존 점프 속도 초기화 (더블 점프 시 중요)
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger(jumpCount == 0 ? "Jump" : "DoubleJump");

        isGrounded = false;
        jumpCount++;
        coyoteTimeCounter = 0; // 점프 시 코요테 타임 리셋
    }

    void Slide()
    {
        isSliding = true;
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
            isGrounded = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("UnderGround"))
        {
            fall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}

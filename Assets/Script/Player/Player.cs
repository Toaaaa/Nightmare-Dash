using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;

    [Header("Player Setting")]
    [SerializeField] float jumpForce = 7f; // 점프 힘
    [SerializeField] int maxJumps = 2; // 최대 점프 횟수
    [SerializeField] float jumpGravity = 3.5f;
    [SerializeField] float fallGravity = 7.5f;
    [SerializeField] PlayerData playerData;
    [Header("Player Ojects & Status")]
    [SerializeField] BoxCollider2D hitbox; // 피격 판정
    [SerializeField] Animator playerFX; // 플레이어 이펙트
    [SerializeField]float maxHp = 100f;
    float currentHp;
    float invincibleTime; // 무적 시간
    float scoreValue; // 점수 획득 배율

    // 이동 및 게임 플레이 관련 변수
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
        //플레이어 데이터 초기화
        maxHp = playerData.GetTotalHp();
        invincibleTime = playerData.GetTotalInvincibleTime();
        scoreValue = playerData.GetTotalScoreValue();
        SetHpMax();
        HitboxSet(0);
    }

    void Update()
    {
        CheckIsDead();
        AdjustGravity();
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

    public void ResetP()
    {
        rb.velocity = Vector2.zero;
        SetHpMax();
        HitboxSet(0);
        maxHp = playerData.GetTotalHp();
        invincibleTime = playerData.GetTotalInvincibleTime();
        scoreValue = playerData.GetTotalScoreValue();
        fall = false;
        isDead = false;
        coyoteTimeCounter = coyoteTime;
        //Hp바 초기화
        GameSceneController gc = SceneBase.Current as GameSceneController;
        gc.uiController.hpBar.GetDmg(0);// 추락시 hp바 0으로 갱신
        //애니메이션 리셋
        animator.SetBool("isSliding", false);
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("DoubleJump");
        animator.SetBool("isFirstEnter", false);
    }
    public void SetFall()// 낙하로 인한 사망
    {
        fall = true;
        currentHp = 0;
        GameSceneController gc = SceneBase.Current as GameSceneController;
        gc.uiController.hpBar.GetDmg(0);// 추락시 hp바 0으로 갱신
    }
    public void HitboxSet(int num)
    {
        if(num == 0)// 일반 상태 hitbox
        {
            hitbox.offset = new Vector2(0, -0.86f);
            hitbox.size = new Vector2(1f, 2.3f);
        }
        else// 슬라이딩 상태 hitbox
        {
            hitbox.offset = new Vector2(0, -1.7f);
            hitbox.size = new Vector2(1f, 0.6f);
        }
    }// 충돌 판정 설정
    public void GroundSmokeFX()
    {
        playerFX.SetTrigger("GroundFX");
    }
    public float GetMaxHp()
    {
        return maxHp;
    }
    public float GetCurrentHp()
    {
        return currentHp;
    }
    public float GetScoreValue()
    {
        return scoreValue;
    }// 플레이어가 코인을 획득할시 얻는 점수의 배율.
    public void SetHpMax()
    {
        currentHp = maxHp;
    }
    public void GetDmg(int dmg)
    {
        currentHp -= dmg;
        float hppercent = currentHp / maxHp;
        GameSceneController gc = SceneBase.Current as GameSceneController;
        gc.uiController.hpBar.GetDmg(hppercent);// 데미지 받을시 hp바 갱신
    }

    void CheckIsDead()
    {
        isDead =  currentHp <= 0 ? true : false;
    }
    void AdjustGravity()
    {
        if(rb.velocity.y < 0)
        {
            rb.gravityScale = fallGravity;
        }
        else
        {
            rb.gravityScale = jumpGravity;
        }
    }// 점프시와 떨어질때의 중력 조절


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
            SetFall();
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

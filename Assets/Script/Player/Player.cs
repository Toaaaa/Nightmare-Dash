using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public PlayerItemSound playerItemSound;

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
    bool isInvincible = false;
    int jumpCount = 0;
    float scoreValue; // 점수 획득 배율

    // 이동 및 게임 플레이 관련 변수
    float slopeSpeed = 1.2f; // 경사면 속도
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
        playerItemSound = GetComponentInChildren<PlayerItemSound>();
        //플레이어 데이터 초기화
        maxHp = playerData.GetTotalHp();
        invincibleTime = playerData.GetTotalInvincibleTime();
        scoreValue = playerData.GetTotalScoreValue();
        SetHpMax();
        HitboxSet(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            currentHp = 10;
        }
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
            rb.velocity = new Vector2(slopeSpeed, rb.velocity.y);// 경사면 이동 보정
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
        isInvincible = false;
        maxJumps = 2;
        fall = false;
        isDead = false;
        coyoteTimeCounter = coyoteTime;
        //Hp바 초기화
        GameSceneController gc = SceneBase.Current as GameSceneController;
        gc.uiController.hpBar.UpdateHpBar(100);
        //애니메이션 리셋
        animator.SetBool("isSliding", false);
        animator.SetTrigger("SetAlive");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("DoubleJump");
        animator.SetBool("isFirstEnter", false);
    }// 게임 재시작시 플레이어 초기화

    // 아이템 획득시 효과 적용
    public void Heal(float heal)
    {
        currentHp += heal;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }// 회복 아이템 획득시 체력 회복
    public async void Invinsible()
    {
        await InvinsibleSet();
    }// 무적 아이템 획득시 무적 상태로 전환
    async UniTask InvinsibleSet()
    {
        isInvincible = true;
        await UniTask.Delay((int)invincibleTime * 1000);
        isInvincible = false;
    }
    public async void TripleJumpActivate()// 5초간 3단 점프 활성화
    {
        await TripleJumpSet();
    }
    async UniTask TripleJumpSet()
    {
        maxJumps = 3;
        await UniTask.Delay(5000);
        maxJumps = 2;
    }

    // 기타 플레이어 상태 설정
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
        playerItemSound.PlayCoinSound();// 코인 획득시 사운드 재생
        return scoreValue;
    }// 플레이어가 코인을 획득할시 얻는 점수의 배율.
    public void SetHpMax()
    {
        currentHp = maxHp;
    }
    public void GetDmg(int dmg)
    {
        if(isInvincible) return;// 무적 상태일때는 데미지를 받지 않음
        // 피격시 깜빡임
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;

        // 빨간색 → 원래 색 두 번 깜빡이기
        DOTween.Sequence()
            .Append(spriteRenderer.DOColor(Color.red, 0.1f))
            .Append(spriteRenderer.DOColor(originalColor, 0.1f))
            .Append(spriteRenderer.DOColor(Color.red, 0.1f))
            .Append(spriteRenderer.DOColor(originalColor, 0.1f));

        // 연산 적용
        currentHp -= dmg;
        float hppercent = currentHp / maxHp;
        GameSceneController gc = SceneBase.Current as GameSceneController;
        gc.uiController.hpBar.GetDmg(hppercent);// 데미지 받을시 hp바 갱신
    }// 데미지 연산

    void CheckIsDead()
    {
        if(!isDead && currentHp <= 0) // 체력이 0이 되는 순간.
        {
            isDead = true;
            animator.SetTrigger("Die");
        }
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

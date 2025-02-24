using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float jumpForce = 5f;
    public float runSpeed = 3f;
    public float slideSpeed = 6f;
    public float slideDuration = 0.5f;
    public float slideCooldownTime = 0.2f;
    public bool isDead = false;
    float deathCooldown = 0f;
    float slideCooldown = 0f;
    float slideTimer = 0f;

    bool isJump = false;
    bool isGrounded = false;
    bool isSliding = false;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDead)
        {
            if(deathCooldown <= 0)
            {

            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
            {
                isJump = true;
            }

            if(Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && slideCooldown <= 0)
            {
                StartSlide();
            }
        }

        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isRunning", !isSliding && runSpeed > 0);
        animator.SetBool("isSliding", isSliding);
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;

        if (isSliding)
        {
            velocity.x = slideSpeed;
        }

        else
        {
            velocity.x = runSpeed;
        }

        if (isJump)
        {
            velocity.y = jumpForce;
            isJump = false;
            isGrounded = false;
        }

        _rigidbody.velocity = velocity;

        if (isSliding)
        {
            slideTimer -= Time.fixedDeltaTime;
            if(slideTimer <= 0)
            {
                Endslide();
            }
        }

        if(slideCooldown > 0)
        {
            slideCooldown -= Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isDead) return;
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        
        deathCooldown = 1f;
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        slideCooldown = slideCooldownTime + slideDuration;
    }

    void Endslide()
    {
        isSliding = false;
    }

    public void OnSlideAnimationEnd()
    {
        Endslide();
    }
}

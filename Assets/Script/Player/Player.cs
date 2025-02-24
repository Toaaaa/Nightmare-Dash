using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D _rigidbody;

    public float jumpForce = 5f;
    public float runSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isJump = false;
    bool isGrounded = false;
    
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
        }

        animator.SetBool("isJumping", !isGrounded);
        animator.SetBool("isRunning", runSpeed > 0);
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = runSpeed;

        if (isJump)
        {
            velocity.y = jumpForce;
            isJump = false;
            isGrounded = false;
        }

        _rigidbody.velocity = velocity;
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
}

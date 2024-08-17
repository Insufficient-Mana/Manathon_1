using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Jump : MonoBehaviour
{

    [SerializeField] BoxCollider2D groundDetector;
    [SerializeField] float jumpStrength;
    [SerializeField] float maxJumpHeight;
    [SerializeField] float maxFallSpeed;

    Rigidbody2D myRigidbody2D;

    public bool isOnGround;
    public bool isJumping;
    public bool isBouncing;
    public bool canJump;

    public AudioSource jumpSound;
    public AudioSource landSound;

    float currentJumpHeight = 0;

    Animator myAnimator;

    private void Awake()
    {
        if (!groundDetector.isTrigger)
        {
            groundDetector.isTrigger = true;
        }

        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        canJump = true;
    }

    private void Update()
    {
        if (canJump)
        {
            if (!isBouncing)
            {
                //begin jump
                if (isOnGround && !isJumping && Input.GetKeyDown(KeyCode.Space))
                {
                    StartJump();
                }

                //do jump
                if (isJumping && Input.GetKey(KeyCode.Space))
                {
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpStrength);
                    currentJumpHeight += jumpStrength * Time.deltaTime;
                }

                //end jump if key released
                if (isJumping && !Input.GetKey(KeyCode.Space))
                {
                    EndJump();
                }

                //end jump if max height is reached
                if (isJumping && currentJumpHeight >= maxJumpHeight)
                {
                    EndJump();
                }

                //clamp fall speed
                if (myRigidbody2D.velocity.y <= -maxFallSpeed)
                {
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -maxFallSpeed);
                }
            }
            else
            {
                currentJumpHeight += jumpStrength * Time.deltaTime;
                if (isOnGround)
                {
                    currentJumpHeight = 0;
                    myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpStrength * 2.1f);
                }
                else
                {
                    isBouncing = false;
                }
            }

            myAnimator.SetBool("IsOnGround", isOnGround);
            myAnimator.SetBool("IsJumping", isJumping);
        }
    }

    private void StartJump()
    {
        isJumping = true;
        currentJumpHeight = 0;
        jumpSound.Play();
    }

    public void EndJump()
    {
        isJumping = false;
        isBouncing = false;
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isOnGround = true;
            landSound.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            isOnGround = false;
        }
    }
}

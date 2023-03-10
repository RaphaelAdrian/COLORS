using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    public delegate void CharacterEvent();

    public CharacterEvent OnJump;
    public float moveSpeed = 10f;
    public float jumpSpeed = 10f;
    internal string horizontalInput = "Horizontal";
    internal string verticalInput = "Horizontal";
    internal KeyCode jumpKey = KeyCode.Space ;

    [Header("Checking Ground")]
    public Transform groundChecker; 
    public LayerMask groundLayer;
    float checkGroundRadius; 

    float moveX = 0;
    float moveY = 0;

    bool isGrounded;
    bool isJumping;

    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        checkGroundRadius = groundChecker.localScale.x;

        // Listeners
        OnJump += Jump;
    }


    // Update is called once per frame
    void Update()
    {        
        isGrounded = CheckIfGrounded();
        moveX = Input.GetAxis(horizontalInput);
        moveY = Input.GetAxis(verticalInput);

        if (isGrounded) {
            if (Input.GetKeyDown(jumpKey)) {
                OnJump(); 
            }
        
            //set jumping to false when landed
            if (isJumping && rb.velocity.y < 0.1f) {
                isJumping = false;
            }
        }

        HandleAnimation();
    }

    private void Jump() {
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }



    private void HandleAnimation()
    {
        bool isWalking = moveX != 0 && isGrounded;
        animator.SetBool("isWalk", isWalking);
        animator.SetBool("isJump", isJumping);

        int faceDirection = Math.Sign(moveX);
        
        if (faceDirection != 0) {
            transform.localScale = new Vector3(faceDirection, transform.localScale.y, transform.localScale.z);
        }
    }

    private bool CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundChecker.position, checkGroundRadius, groundLayer); 
        if (collider != null) { 
            return true; 
        }

        return false; 
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(moveX * moveSpeed , rb.velocity.y);
    }
}

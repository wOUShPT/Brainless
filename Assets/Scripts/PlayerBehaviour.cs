using System;
using System.Collections;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject Player;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float HorizontalVelocity = 3;
    public float JumpVelocity = 2;
    public float horizontalJumpForce;
    public float groundContactRaySpread;
    [Range(0f, 0.1f)] public float GroundContactTolerance = 0.005f;
    private bool onGround;
    private bool OnWall;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerCollider;
    private float HorizontalInputDirection;
    private int faceDirection = 1;
    public bool canPlayerMove;
    private bool isContactingFront;
    public float checkRadius;
    public Vector3 wallCheckOffSet;
    public float wallJumpDuration;
    public float wallSlideSpeed;
    private bool jumpFromWall;
    private bool jumpPressed;
    private float wallJumpFinishTime;
    private float wallSlidingSpeed;
    private bool resetInputs;
    public string enemyTag;
    void Awake()
    {
        playerRigidBody = Player.GetComponent<Rigidbody2D>();
        playerCollider = playerRigidBody.GetComponent<BoxCollider2D>();
        onGround = false;
        OnWall = false;
        canPlayerMove = true;
        resetInputs = false;
        jumpFromWall = false;
    }

    void FixedUpdate()
    {
        HorizontalMovement();
        Jump();
        resetInputs = true;
    }

    void Update()
    {
        Inputs();
        CheckContacts();
    }

    private void HorizontalMovement()
    {
        if (canPlayerMove)
        {
            playerRigidBody.velocity = new Vector2(HorizontalInputDirection * HorizontalVelocity, playerRigidBody.velocity.y);

            if (HorizontalInputDirection * faceDirection < 0f)
            {
                FlipPlayer();
            }
        }
    }

    private void Jump()
    {
        if (jumpPressed && onGround)
        {
            jumpPressed = false;
            Vector2 jumpForce = new Vector2(0, JumpVelocity);
            playerRigidBody.velocity = Vector2.zero;
            playerRigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
        else if(jumpPressed && OnWall && !onGround)
        {
            jumpPressed = false;
            canPlayerMove = false;
            wallJumpFinishTime = Time.time + wallJumpDuration;
            jumpFromWall = true;
            FlipPlayer();
            
            playerRigidBody.velocity = Vector2.zero;
            playerRigidBody.AddForce(new Vector2(horizontalJumpForce * faceDirection, JumpVelocity), ForceMode2D.Impulse);
        }
    }

    private void Inputs()
    {
        if (resetInputs)
        {
            jumpPressed = false;
            resetInputs = false;
        }

       
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");

        if (canPlayerMove)
        {
            HorizontalInputDirection = Input.GetAxis("Horizontal");
        }

        if (jumpFromWall)
        {
            if(Time.time > wallJumpFinishTime)
            {
                jumpFromWall = false;
            }
        }

        if(!jumpFromWall && !canPlayerMove)
        {
            if(Input.GetAxis("Horizontal") != 0 || onGround)
            {
                canPlayerMove = true;
            }
        }
    }

    private void FlipPlayer()
    {
        faceDirection *= -1;
    }
    
    private void CheckContacts()
    {
        onGround = false;
        OnWall = false;
        
        Vector2 origin = playerCollider.transform.position;
        Vector2 direction = Vector2.down;
        Vector2 originOffSet = new Vector2(origin.x, origin.y);
        RaycastHit2D groundLeftHit = Physics2D.Raycast(new Vector2(originOffSet.x - groundContactRaySpread, originOffSet.y), direction, (playerCollider.size.y / 2) + GroundContactTolerance, groundLayer);
        RaycastHit2D groundRightHit = Physics2D.Raycast(new Vector2(originOffSet.x + groundContactRaySpread, originOffSet.y), direction, (playerCollider.size.y / 2) + GroundContactTolerance, groundLayer);
        Debug.DrawRay(new Vector2(originOffSet.x - groundContactRaySpread, originOffSet.y), direction * ((playerCollider.size.y / 2) + GroundContactTolerance), Color.blue);
        Debug.DrawRay(new Vector2(originOffSet.x + groundContactRaySpread, originOffSet.y), direction * ((playerCollider.size.y / 2) + GroundContactTolerance), Color.blue);

        if (groundLeftHit || groundRightHit)
        {
            Debug.Log("Grounded");
            onGround = true;
        }

        bool leftWallcontact = Physics2D.OverlapCircle(transform.position - wallCheckOffSet, checkRadius, wallLayer);
        bool righWallContact = Physics2D.OverlapCircle(transform.position + wallCheckOffSet, checkRadius, wallLayer);

        if (leftWallcontact || righWallContact)
        {
            OnWall = true;
        }
        
        if (OnWall)
        {
            if(playerRigidBody.velocity.y < wallSlideSpeed)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, wallSlideSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(enemyTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - wallCheckOffSet, checkRadius);
        Gizmos.DrawWireSphere(transform.position + wallCheckOffSet, checkRadius);
    }
}

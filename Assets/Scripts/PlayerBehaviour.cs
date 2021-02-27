using System.Collections;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class PlayerBehaviour : MonoBehaviour
{
    public GameObject Player;
    public LayerMask groundLayer;
    public float HorizontalVelocity = 3;
    public float JumpVelocity = 2;
    public int NumberOfRaysToGround = 3;
    [Range(0f, 0.1f)] public float GroundContactTolerance = 0.005f;
    private bool IsGrounded;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerCollider;
    private float HorizontalInputDirection;
    public bool CanPlayerMove;
    private bool isContactingFront;
    public float checkRadius;
    public float xWallForce;
    public float yWallForce;
    public Vector3 wallCheckOffSet;
    private bool wallSliding;
    private bool wallJumping;
    public float WallSlidingSpeed;
    void Awake()
    {
        playerRigidBody = Player.GetComponent<Rigidbody2D>();
        playerCollider = playerRigidBody.GetComponent<BoxCollider2D>();
        IsGrounded = false;
        CanPlayerMove = true;
    }

    void FixedUpdate()
    {
        if (CanPlayerMove)
        {
            HorizontalMove();
        }
    }

    void Update()
    {
        HorizontalInputDirection = Input.GetAxis("Horizontal");
        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && CanPlayerMove)
            {
                IsGrounded = false;
                Jump();
            }
        }
        
        isContactingFront = Physics2D.OverlapCircle(new Vector2(transform.position.x + HorizontalInputDirection, transform.position.y), checkRadius, groundLayer);

    }

    private void HorizontalMove()
    { 
        playerRigidBody.velocity = new Vector2(HorizontalInputDirection * HorizontalVelocity, playerRigidBody.velocity.y);
    }

    private void Jump()
    {
        Vector2 jumpForce = new Vector2(0, JumpVelocity);
        playerRigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    private bool DetectGround()
    {
        Vector2 origin = playerCollider.transform.position;
        Vector2 direction = Vector2.down;
        Vector2 originOffSet = new Vector2(origin.x, origin.y);
        RaycastHit2D hit = Physics2D.Raycast(originOffSet, direction,
            (playerCollider.size.y / 2) + GroundContactTolerance, groundLayer);
        Debug.DrawRay(originOffSet, direction * ((playerCollider.size.y / 2) + GroundContactTolerance), Color.blue);
        return hit;
    }
    

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        IsGrounded = DetectGround();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }
    
    
}

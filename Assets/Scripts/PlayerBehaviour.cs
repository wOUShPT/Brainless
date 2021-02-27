using System;
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
    public LayerMask wallLayer;
    public float HorizontalVelocity = 3;
    public float JumpVelocity = 2;
    public float groundContactRaySpread;
    [Range(0f, 0.1f)] public float GroundContactTolerance = 0.005f;
    private bool OnGround;
    private bool OnWall;
    private Rigidbody2D playerRigidBody;
    private BoxCollider2D playerCollider;
    private float HorizontalInputDirection;
    public bool CanPlayerMove;
    private bool isContactingFront;
    public float checkRadius;
    public Vector3 wallCheckOffSet;
    private float wallSlidingSpeed;
    void Awake()
    {
        playerRigidBody = Player.GetComponent<Rigidbody2D>();
        playerCollider = playerRigidBody.GetComponent<BoxCollider2D>();
        OnGround = false;
        OnWall = false;
        CanPlayerMove = true;
    }

    void FixedUpdate()
    {
        HorizontalMove();
    }

    void Update()
    {
        HorizontalInputDirection = Input.GetAxis("Horizontal");
        CheckContacts();
        if (Input.GetButtonDown("Jump") && OnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
    }

    private void HorizontalMove()
    {
        if (CanPlayerMove)
        {
            playerRigidBody.velocity = new Vector2(HorizontalInputDirection * HorizontalVelocity, playerRigidBody.velocity.y);
        }
    }

    private void Jump()
    {
        if (CanPlayerMove)
        {
            OnGround = false;
            Vector2 jumpForce = new Vector2(0, JumpVelocity);
            playerRigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }

    private void CheckContacts()
    {
        OnGround = false;
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
            OnGround = true;
        }

        bool leftWallcontact = Physics2D.OverlapCircle(transform.position - wallCheckOffSet, checkRadius, wallLayer);
        bool righWallContact = Physics2D.OverlapCircle(transform.position + wallCheckOffSet, checkRadius, wallLayer);

        if (leftWallcontact || righWallContact)
        {
            OnWall = true;
        }
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position - wallCheckOffSet, checkRadius);
        Gizmos.DrawWireSphere(transform.position + wallCheckOffSet, checkRadius);
    }
}

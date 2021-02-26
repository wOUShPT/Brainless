using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform GroundDetection;
    [SerializeField] private float speed = 4f;
    private float speedDefault;
    [SerializeField] private float groundDetectionRange = 3f;
    [SerializeField]private bool isFacingRight = true;
    [SerializeField] private float chargeSpeed = 10f;
    private float castDistance;
    [SerializeField] private float agroDistance = 5;
    private Rigidbody2D rb;
    [SerializeField]private LayerMask playerMask;
    private bool isSeeingPlayer;
    private BoxCollider2D bc;
    [SerializeField]private LayerMask groundLayerMask;

    private enum States
    {
        Patrol, Charge
    }
    private States states;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        speedDefault = speed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (states)
        {
            case States.Patrol:
                if(speed != speedDefault)
                {
                    speed = speedDefault;
                }
                MoveEnemy();
                CheckGroundDetection();
                if (canSeeThePlayer(agroDistance))
                {
                    states = States.Charge;
                }
                break;
            case States.Charge:
                if(speed != chargeSpeed)
                {
                    speed = chargeSpeed;
                }

                if (!canSeeThePlayer(agroDistance))
                {
                    states = States.Patrol;
                }
                MoveEnemy();
                CheckGroundDetection();
                break;
        }
        
    }
    private void Flip()
    {
        // Is facing the other side
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180f, 0);
    }
    private void MoveEnemy()
    {
        if (isFacingRight)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            // in case enemy is facing left
            rb.velocity = -Vector2.right * speed;
        }
    }
    private void CheckGroundDetection()
    {
        // if the "front" of enemy has ground
        RaycastHit2D raycastHit = Physics2D.Raycast(GroundDetection.position, Vector2.down, groundDetectionRange);
        Debug.DrawLine(GroundDetection.position, GroundDetection.position - new Vector3(0, groundDetectionRange));
        if (raycastHit.collider == false && isGrounded())
        {
            Flip();
        }
        Debug.Log(isGrounded());
    }
    private bool canSeeThePlayer(float distanceRaycast)
    {
        
        if (isFacingRight)
        {
            castDistance = distanceRaycast;
        }
        else
        {
            castDistance = -distanceRaycast;

        }
        Vector2 endPos = GroundDetection.position + Vector3.right * castDistance;

        RaycastHit2D hit = Physics2D.Linecast(GroundDetection.position, endPos, playerMask);
        Color rayColor;
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // Let's agro the enemy
                isSeeingPlayer = true;
            }
            else
            {
                isSeeingPlayer = false;
            }
            
            rayColor = Color.green;
        }
        else
        {
            isSeeingPlayer = false;
            rayColor = Color.red;
        }
        Debug.DrawLine(GroundDetection.position, endPos, rayColor);
        return isSeeingPlayer;
    }
    private bool isGrounded()
    {
        float extraHeight = .05f;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(bc.bounds.center, Vector2.down, bc.bounds.extents.y + extraHeight, groundLayerMask);
        Color color;
        if(raycastHit2D.collider != null)
        {
            // Hit something
            color = Color.green;
        }
        else
        {
            color = Color.red;
        }
        Debug.DrawRay(bc.bounds.center, Vector2.down * (bc.bounds.extents.y + extraHeight), color);
        return raycastHit2D.collider != null;

    }
    
}

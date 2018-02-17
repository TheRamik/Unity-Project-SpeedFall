using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] Animator anim;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField]private float groundRadius;
    [SerializeField] private Transform[] groundPoints;
    public bool isGrounded;

    private Rigidbody2D rgbd;
    bool isMoving, isJumping;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isMoving = false;
        isJumping = false;
        isGrounded = IsGrounded();

        HandleInput();
        HandleMovement(horizontal);
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            isJumping = !isJumping;
            HandleJump();
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            isMoving = !isMoving;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            isMoving = !isMoving;
        }
    }

    private void HandleJump()
    {
        if (isGrounded && isJumping)
        {
            isJumping = false;
            rgbd.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void HandleMovement(float horizontal)
    {
        rgbd.velocity = new Vector2(horizontal * moveSpeed, rgbd.velocity.y);
    }

    private bool IsGrounded()
    {
        if (rgbd.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}

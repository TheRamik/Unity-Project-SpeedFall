using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public bool isGrounded, isWall;
    public GameObject starPrefab;
    public GameObject starSpawner;
    private Rigidbody2D rgbd;
    bool isMoving, isJumping, isWallSliding, facingRight;

    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] Animator anim;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField]private float groundRadius;
    [SerializeField] private Transform[] groundPoints;
    public Transform wallPoint;
    public LayerMask wallLayerMask;

    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        facingRight = true;
    }

    private void Update()
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
            facingRight = !facingRight;
            isMoving = !isMoving;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            facingRight = !facingRight;
            isMoving = !isMoving;
        }
        if (Input.GetKey(KeyCode.K))
        {
            ShootingStar();
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

    private void HandleWallSliding(float horizontal)
    {
        if (!isGrounded)
        {
            isWall = Physics2D.OverlapCircle(wallPoint.position, 0.1f, wallLayerMask);
            if ((facingRight && (horizontal > 0.1f)) || (!facingRight && (horizontal < 0.1f)))
            {
                if (isWall)
                {
                    rgbd.velocity = new Vector2(rgbd.velocity.x, -0.7f);
                    isWallSliding = true;
                }
            }
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

    void ShootingStar()
    {
        if (facingRight)
        {
            GameObject star = (GameObject)Instantiate(starPrefab, transform.position, Quaternion.identity);
            star.transform.position = starSpawner.transform.position;
            star.GetComponent<NinjaStar>().Initialize(Vector2.right);
            
        }
        else
        {
            GameObject star = (GameObject)Instantiate(starPrefab, transform.position, Quaternion.identity);
            star.transform.position = starSpawner.transform.position;
            star.GetComponent<NinjaStar>().Initialize(Vector2.left);
        }
    }
}
